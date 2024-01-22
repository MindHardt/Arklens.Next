﻿using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
// ReSharper disable ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator

namespace Arklens.Next.Generators;

[Generator]
public class AlidDictionaryGenerator : IIncrementalGenerator
{
    private const string Namespace = "Arklens.Next.Core";
    private const string AlidClassName = "AlidEntity";
    private const string AlidDomainAttributeName = "AlidDomainAttribute";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var provider = context.SyntaxProvider.CreateSyntaxProvider(
                (node, _) => node is TypeDeclarationSyntax, Transform)
            .Where(x => x.shouldInclude)
            .Select((x, _) => x.type);

        context.RegisterSourceOutput(context.CompilationProvider.Combine(provider.Collect()),
            (ctx, t) => GenerateCode(ctx, t.Left, t.Right));
    }

    private static (TypeDeclarationSyntax type, bool shouldInclude) Transform(
        GeneratorSyntaxContext ctx,
        CancellationToken ct)
    {
        var type = (TypeDeclarationSyntax)ctx.Node;

        foreach (var attributeList in type.AttributeLists)
        foreach (var attribute in attributeList.Attributes)
        {
            ct.ThrowIfCancellationRequested();

            if (ctx.SemanticModel.GetSymbolInfo(attribute).Symbol is not IMethodSymbol attributeSymbol)
                continue;

            var attributeName = attributeSymbol.ContainingType.ToDisplayString();

            // Check the full name of the [Report] attribute.
            if (attributeName == $"{Namespace}.{AlidDomainAttributeName}")
                return (type, true);
        }

        return (type, false);
    }

    private static void GenerateCode(
        SourceProductionContext context,
        Compilation compilation,
        ImmutableArray<TypeDeclarationSyntax> typeDeclarations)
    {
        var propertyNames = typeDeclarations
            .Select(x => compilation
                .GetSemanticModel(x.SyntaxTree)
                .GetDeclaredSymbol(x)!)
            .Select(x => $"{x.ToDisplayString()}.{EnumerationGenerator.InterfaceProperty}");
        var code =
              $$"""
                // <auto-generated/>
                #nullable enable

                using global::System.Collections;
                using global::System.Collections.Generic;
                using global::System.Collections.Frozen;
                using global::Arklens.Next.Core;
                using global::System.Diagnostics.CodeAnalysis;

                namespace {{Namespace}};

                [global::System.CodeDom.Compiler.GeneratedCode("{{nameof(AlidDictionaryGenerator)}}", "1.0")]
                partial record {{AlidClassName}} 
                    : {{EnumerationGenerator.Namespace}}.{{EnumerationGenerator.InterfaceName}}<{{AlidClassName}}>
                {
                    private static readonly FrozenDictionary<string, AlidEntity> _allEntitiesDictionary = CreateDictionary();
                  
                    public static IReadOnlyCollection<AlidEntity> AllValues => _allEntitiesDictionary.Values;
                    public static partial AlidEntity? Get(string alid)
                        => _allEntitiesDictionary.GetValueOrDefault(alid);
                    
                    private static FrozenDictionary<string, AlidEntity> CreateDictionary()
                    {
                        AlidEntity[] allEntities =
                        [
                {{string.Join(",\n", propertyNames.Select(x => $"\t\t\t..{x}"))}}
                        ];
                        return allEntities.ToFrozenDictionary(x => x.Alid.Text);
                    }
                }
                """;

        context.AddSource($"{AlidClassName}.g.cs", SourceText.From(code, Encoding.UTF8));
    }
}
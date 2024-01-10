﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Arklens.Next.Generators;

[Generator]
public class SourceGeneratedAlidSearchGenerator : IIncrementalGenerator
{
    private const string Namespace = nameof(SourceGeneratedAlidSearchGenerator);
    private const string TypeName = "SourceGeneratedAlidSearch";

    public const string AttributeShortName = "SearchInclude";
    public const string AttributeFullName = AttributeShortName + nameof(Attribute);
    public const string AttributeSourceText =
        $$"""
          // <auto-generated>

          namespace {{Namespace}}
          {
            /// <summary>
            /// Indicates that decorated class is included in source-generated <see cref="{{TypeName}}"/>.
            /// The decorated types should implement <see cref="EnumerationGenerator.IEnumeration{T}"/>.
            /// </summary>
            [global::System.CodeDom.Compiler.GeneratedCode("{{nameof(SourceGeneratedAlidSearchGenerator)}}", "1.0")]
            [global::System.AttributeUsage(global::System.AttributeTargets.Class | global::System.AttributeTargets.Struct)]
            public class {{AttributeFullName}} : global::System.Attribute
            {
            }
          }
          """;

    private const string AlidEntityInterfaceName =
        "Arklens.Next.Core.IAlidEntity";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(ctx =>
        {
            ctx.AddSource($"{AttributeFullName}.g.cs", SourceText.From(AttributeSourceText, Encoding.UTF8));
        });

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

        // Go through all attributes of the class.
        foreach (var attributeList in type.AttributeLists)
        foreach (var attribute in attributeList.Attributes)
        {
            ct.ThrowIfCancellationRequested();

            if (ctx.SemanticModel.GetSymbolInfo(attribute).Symbol is not IMethodSymbol attributeSymbol)
                continue;

            var attributeName = attributeSymbol.ContainingType.ToDisplayString();

            // Check the full name of the [Report] attribute.
            if (attributeName == $"{Namespace}.{AttributeFullName}")
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
            .Select(x => $"\t..{x.ToDisplayString()}.{EnumerationGenerator.InterfaceProperty}");
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

              [global::System.CodeDom.Compiler.GeneratedCode("{{nameof(SourceGeneratedAlidSearchGenerator)}}", "1.0")]
              public class {{TypeName}} : global::Arklens.Next.Search.IAlidSearch
              {
                private static readonly IReadOnlyCollection<AlidEntity> IncludedEnumerations = 
                [
              {{string.Join(",\n", propertyNames)}}
                ];
              
                private readonly FrozenDictionary<Alid, AlidEntity> _innerDictionary;
                
                public static {{TypeName}} Instance { get; } = new();
                
                private {{TypeName}}()
                {
                    _innerDictionary = IncludedEnumerations.ToFrozenDictionary(x => x.Alid, x => x);
                }
                  
                public AlidEntity? Get(Alid alid)
                    => _innerDictionary.GetValueOrDefault(alid);
                    
                public IReadOnlyCollection<AlidEntity> IncludedEntities => _innerDictionary.Values;
              }
              """;

        context.AddSource($"{TypeName}.g.cs", SourceText.From(code, Encoding.UTF8));
    }
}
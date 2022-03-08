using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeBlockSourceGenerator;

[Generator]
public class Generator : ISourceGenerator
{
    private class AttributeSyntaxReceiver<TAttribute> : ISyntaxReceiver where TAttribute : Attribute
    {
        public IList<ClassDeclarationSyntax> Classes { get; } = new List<ClassDeclarationSyntax>();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is ClassDeclarationSyntax classDeclarationSyntax &&
                classDeclarationSyntax.AttributeLists.Count > 0 &&
                classDeclarationSyntax.AttributeLists
                    .Any(al => al.Attributes
                        .Any(a => a.Name.ToString().EnsureEndsWith("Attribute").Equals(typeof(TAttribute).Name))))
            {
                Classes.Add(classDeclarationSyntax);
            }
        }
    }

    public void Initialize(GeneratorInitializationContext context)
    {
    }

    public void Execute(GeneratorExecutionContext context)
    {
#if DEBUG
        if (!Debugger.IsAttached)
        {
            Debugger.Launch();
        }
#endif 

        var compilation = context.Compilation;
        var cSharp = compilation as CSharpCompilation;

        var enumValidatorType = compilation.GetTypeByMetadataName("RecipeInterface.IngredientParsing.CodeIngredient");
        var info = GetEnumValidationInfo(compilation, enumValidatorType).ToArray();
        var source = "class Foo { }";

        if (source != null)
        {
            context.AddSource("generated.cs", source);
        }
    }

    private static IEnumerable<EnumValidationInfo> GetEnumValidationInfo(Compilation compilation, INamedTypeSymbol enumValidatorType)
    {
        foreach (SyntaxTree tree in compilation.SyntaxTrees)
        {
            var semanticModel = compilation.GetSemanticModel(tree);
            foreach (var invocation in tree.GetRoot().DescendantNodesAndSelf())
            {
                var symbol = semanticModel.GetSymbolInfo(invocation).
                if (symbol == null)
                {
                    continue;
                }

                if (SymbolEqualityComparer.Default.Equals(symbol.ContainingType, enumValidatorType))
                {
                    // Note: This assumes the only method on enumValidatorType is the one we want.
                    // ie, I'm too lazy to check which invocation is being made :)
                    //var argument = invocation.ArgumentList.Arguments.First().Expression;
                    //var enumType = semanticModel.GetTypeInfo(argument).Type;
                    //if (enumType == null)
                    //{
                    //    continue;
                    //}

                    //var info = new EnumValidationInfo(enumType, argument.ToString());
                    //foreach (var member in enumType.GetMembers())
                    //{
                    //    if (member is IFieldSymbol
                    //        {
                    //            IsStatic: true,
                    //            IsConst: true,
                    //            ConstantValue: int value
                    //        } field)
                    //    {
                    //        info.Elements.Add((field.Name, value));
                    //    }
                    //}

                    //info.Elements.Sort((e1, e2) => e1.Value.CompareTo(e2.Value));

                    //yield return info;
                    yield return null;
                }
            }
        }
    }

    private class EnumValidationInfo
    {
        public List<(string Name, int Value)> Elements = new();

        public ITypeSymbol EnumType { get; }
        public string ArgumentName { get; internal set; }

        public EnumValidationInfo(ITypeSymbol enumType, string argumentName)
        {
            this.EnumType = enumType;
            this.ArgumentName = argumentName;
        }
    }

    //    public void Execute(GeneratorExecutionContext context)
    //    {
    //#if DEBUG
    //        if (!Debugger.IsAttached)
    //        {
    //            Debugger.Launch();
    //        }
    //#endif 

    //        if (context.SyntaxReceiver is not AttributeSyntaxReceiver<GenerateServiceAttribute> syntaxReceiver)
    //        {
    //            return;
    //        }

    //        foreach (var classSyntax in syntaxReceiver.Classes)
    //        {
    //            // Converting the class to semantic model to access much more meaningful data.
    //            var model = context.Compilation.GetSemanticModel(classSyntax.SyntaxTree);
    //            // Parse to declared symbol, so you can access each part of code separately, such as interfaces, methods, members, contructor parameters etc.
    //            var symbol = model.GetDeclaredSymbol(classSyntax);

    //            //// Finding my GenerateServiceAttribute over it. I'm sure this attribute is placed, because my syntax receiver already checked before.
    //            //// So, I can surely execute following query.
    //            //var attribute = classSyntax.AttributeLists.SelectMany(sm => sm.Attributes).First(x => x.Name.ToString().EnsureEndsWith("Attribute").Equals(typeof(GenerateServiceAttribute).Name));

    //            //// Getting constructor parameter of the attribute. It might be not presented.
    //            //var templateParameter = attribute.ArgumentList?.Arguments.FirstOrDefault()?.GetLastToken().ValueText; // Temprorary... Attribute has only one argument for now.

    //            //// Can't access embeded resource of main project.
    //            //// So overridden template must be marked as Analyzer Additional File to be able to be accessed by an analyzer.
    //            //var overridenTemplate = templateParameter != null ?
    //            //    context.AdditionalFiles.FirstOrDefault(x => x.Path.EndsWith(templateParameter))?.GetText().ToString() :
    //            //    null;

    //            //// Generate the real source code. Pass the template parameter if there is a overriden template.
    //            //var sourceCode = GetSourceCodeFor(symbol, overridenTemplate);

    //            //context.AddSource(
    //            //    $"{symbol.Name}{templateParameter ?? "Controller"}.g.cs",
    //            //    SourceText.From(sourceCode, Encoding.UTF8));
    //            //Console.WriteLine(classSyntax);
    //        }
    //    }

    //    private string GetSourceCodeFor(INamedTypeSymbol symbol, string template = null)
    //    {
    //        // If template isn't provieded, use default one from embeded resources.
    //        template ??= GetEmbededResource("Awesome.Generators.Templates.Default.txt");

    //        // Can't use scriban at the moment, make it manually for now.
    //        return template;
    //    }

    //    private string GetEmbededResource(string path)
    //    {
    //        using var stream = GetType().Assembly.GetManifestResourceStream(path);

    //        using var streamReader = new StreamReader(stream);

    //        return streamReader.ReadToEnd();
    //    }

    //    private string GetNamespaceRecursively(INamespaceSymbol symbol)
    //    {
    //        if (symbol.ContainingNamespace == null)
    //        {
    //            return symbol.Name;
    //        }

    //        return (GetNamespaceRecursively(symbol.ContainingNamespace) + "." + symbol.Name).Trim('.');
    //    }
}

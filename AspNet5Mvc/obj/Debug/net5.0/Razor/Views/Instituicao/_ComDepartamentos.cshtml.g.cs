#pragma checksum "C:\GitHubGeral\AspNetGeral\AspNet5Mvc\AspNet5Mvc\AspNet5Mvc\Views\Instituicao\_ComDepartamentos.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "67070b91988867b84dcfdee913843ae4fa49801e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Instituicao__ComDepartamentos), @"mvc.1.0.view", @"/Views/Instituicao/_ComDepartamentos.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\GitHubGeral\AspNetGeral\AspNet5Mvc\AspNet5Mvc\AspNet5Mvc\Views\_ViewImports.cshtml"
using AspNet5Mvc;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\GitHubGeral\AspNetGeral\AspNet5Mvc\AspNet5Mvc\AspNet5Mvc\Views\_ViewImports.cshtml"
using AspNet5Mvc.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"67070b91988867b84dcfdee913843ae4fa49801e", @"/Views/Instituicao/_ComDepartamentos.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4d09be905afbcfbca4387a87f1759e2cce8b2e9c", @"/Views/_ViewImports.cshtml")]
    public class Views_Instituicao__ComDepartamentos : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Core.Entities.Departamento>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<div class=""panel	panel-primary"">
    <div class=""card-header	text-white	bg-warning	text-center"">
        Relação	de	DEPARTAMENTOS	registrados	para	a	instituição
    </div>
    <div class=""panel-body"">
        <table class=""table	table-striped table-hover"">
            <thead>
                <tr>
                    <th>
                        ");
#nullable restore
#line 12 "C:\GitHubGeral\AspNetGeral\AspNet5Mvc\AspNet5Mvc\AspNet5Mvc\Views\Instituicao\_ComDepartamentos.cshtml"
                   Write(Html.DisplayNameFor(model => model.Nome));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                </tr>\r\n            </thead>\r\n            <tbody>\r\n");
#nullable restore
#line 17 "C:\GitHubGeral\AspNetGeral\AspNet5Mvc\AspNet5Mvc\AspNet5Mvc\Views\Instituicao\_ComDepartamentos.cshtml"
                 foreach (var item in Model)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <td>\r\n                            ");
#nullable restore
#line 21 "C:\GitHubGeral\AspNetGeral\AspNet5Mvc\AspNet5Mvc\AspNet5Mvc\Views\Instituicao\_ComDepartamentos.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Nome));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                    </tr>\r\n");
#nullable restore
#line 24 "C:\GitHubGeral\AspNetGeral\AspNet5Mvc\AspNet5Mvc\AspNet5Mvc\Views\Instituicao\_ComDepartamentos.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tbody>\r\n        </table>\r\n    </div>\r\n    <div class=\"panel-footer panel-info\">\r\n    </div>\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Core.Entities.Departamento>> Html { get; private set; }
    }
}
#pragma warning restore 1591

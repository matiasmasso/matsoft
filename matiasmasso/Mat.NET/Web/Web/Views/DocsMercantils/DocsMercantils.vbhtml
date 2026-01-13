@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)
    Dim exs As New List(Of Exception)

    Dim sTitle As String = Mvc.ContextHelper.Tradueix("Documentación Mercantil", "Documentació mercantil", "Merchant documentation")
    ViewBag.Title = sTitle
End Code

<div class="PageWrapperVertical">
    <h3>@sTitle</h3>

    <div class="DocMercantil">
        <a href="@FEB2.Escriptura.UrlConstitucio(exs, Mvc.GlobalVariables.Emp, Mvc.ContextHelper.Domain())" target="_blank">
            @Mvc.ContextHelper.Tradueix("Escritura de Constitución", "Escriptura de Constitució", "Deed of Constitution")
        </a>
    </div>
    <div class="DocMercantil">
        <a href="@FEB2.Escriptura.UrlAdaptacioEstatuts(exs, Mvc.GlobalVariables.Emp, Mvc.ContextHelper.Domain())" target="_blank">
            @Mvc.ContextHelper.Tradueix("Adaptación de Estatutos", "Adaptació d'Estatuts", "Statutes Amendment")
        </a>
    </div>
    <div class="DocMercantil">
        <a href="@FEB2.Escriptura.UrlPoders(exs, Mvc.GlobalVariables.Emp, Mvc.ContextHelper.Domain())" target="_blank">
            @Mvc.ContextHelper.Tradueix("Escritura de Poderes", "Escriptura de Poders", "Power of Attorney")
        </a>
    </div>
    <div class="DocMercantil">
        <a href="@FEB2.Escriptura.UrlTitularitatReal(exs, Mvc.GlobalVariables.Emp, Mvc.ContextHelper.Domain())" target="_blank">
            @Mvc.ContextHelper.Tradueix("Acta notarial de Titularidad Real", "Acta Notarial de Titularitat Real", "Beneficial Owner Entitlement")
        </a>
    </div>
    <div class="DocMercantil">
        <a href="/doc/9/4230394e4a467a68642b532b6a393443695a663541673d3d" target="_blank">
            @Mvc.ContextHelper.Tradueix("Acreditación NIF", "Acreditació NIF", "VAT number certification")
        </a>
    </div>
    <div class="DocMercantil">
        <a href="/mmc" target="_blank">
            @Mvc.ContextHelper.Tradueix("DNI administrador", "DNI administrador", "Administrator Id document")
        </a>
    </div>
    <!--
    <div class="DocMercantil">
        <a href="@@FEB2.DocFile.DownloadUrl(FEB2.AeatDoc.LastFromModelSync(Mvc.GlobalVariables.Emp, DTOAeatModel.Cods.Auditoria, exs).DocFile, False)" target="_blank">
            @@Mvc.ContextHelper.Tradueix("Cuentas Anuales Auditadas", "Comptes Anuals Auditats", "Auditor's Report Annual Accounts")
        </a>
    </div>
        -->
</div>


    @Section Styles
        <style>
            .ContentColumn {
                max-width: 320px;
                margin: 0 auto;
            }


            .DocMercantil {
                margin: 10px 0 10px;
            }
        </style>
    End Section

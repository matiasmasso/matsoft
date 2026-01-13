@ModelType DTOContact
@Code
    Layout = "~/Views/Shared/_Layout_SideNav.vbhtml"

    Dim exs As New List(Of Exception)
    FEB.Contact.Load(Model, exs)
    Dim sTitle As String = String.Format(ContextHelper.Tradueix("Informes de visitas comerciales de {0}", "Raports de visites comercials de {0}", "Sales visits reports to {0}"), Model.FullNom)
    ViewBag.Title = "M+O | " & sTitle
    Dim oUser = ContextHelper.FindUserSync()
    Dim oMems As List(Of DTOMem) = FEB.Mems.AllSync(exs, oContact:=Model, oCod:=DTOMem.Cods.Rep, oUser:=oUser)
End Code


<div class="pagewrapper">
    <div class="PageTitle">@sTitle</div>
    @If oMems.Count = 0 Then
        @<div>
            @ContextHelper.Tradueix("No nos constan visitas registradas", "No ens consten visites registrades", "No visits have been logged")
        </div>
    Else
        @For each oMem As DTOMem In oMems
            @<div>
                <div class="fch">@oMem.Fch.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"))</div>
                <div class="text">@oMem.Text</div>
            </div>
        Next
    End If
</div>

@Section styles
    <style>
        .pagewrapper {
            max-width:800px;
        }
        .fch {
            margin: 20px 10px;
            color:gray;
        }
        .text {
            margin: 10px 30px;
        }
    </style>
End Section


@Code
    Dim oWebSession As Mvc.WebSession = Session("WebSession")

End Code




@If oQuery.Unauthorized Then
    @<p>
         @oWebSession.Tradueix("Modelo", "Model", "Form")


#End ExternalSource

#ExternalSource("C:\Users\matias\Source\Workspaces\MMO\Web\Web\Views\incidencia\Incidencias_.vbhtml",3)
       __o = oWebSession.Tradueix("Concepto", "Concepte", "Concept")
         <br />
         @BLL.BLLAeatModel.Url(item)


#End ExternalSource

#ExternalSource("C:\Users\matias\Source\Workspaces\MMO\Web\Web\Views\incidencia\Incidencias_.vbhtml",6)
               __o = item.Tr Each it"))
    </p>
ElseIf oQuery.result.Count = 0 Then
    @<p>
        @Html.Raw(oWebSession.Tradueix("No existen incidencias abiertas en estos momentos", "No hi han incidencies obertes en aquests moments", "No open support incidences available"))
    </p>
Else
    For i As Integer = oPagination.CurrentPageFirstItem To oPagination.CurrentPageLastItem
        Dim item As DTO.DTOIncidencia = oQuery.result(i)
        @<a href="#" class="Incidencia_row" data-guid='@item.Guid.ToString'>
            <div class="Incidencia_id">
                @item.Id
            </div>
             <div class="Incidencia_Fch">
                 @item.Fch.ToString("dd/MM/yy")
             </div>
             <div class="Incidencia_Img">
                 @If item.ExistImages Then
                    @<img src="/Media/Img/Ico/Camera.png" />
                 End If
             </div>
            <div class="Incidencia_Product truncate">
                @BLL.BLLProduct.FullNom(item.Product)
            </div>
            <div class="Incidencia_Codi truncate">
                @MaxiSrvr.BLL_Incidencia.CodiNom(item.Codi, oWebSession)
            </div>
            <div class="Incidencia_FchClosed">
                @If item.FchClose <> Nothing Then
                        Html.Raw(item.FchClose.ToString("dd/MM/yy"))
                    End If
            </div>
        </a>
Next

        @<div class="pagination">
            @If oPagination.IsDisplayable Then
                @Html.Raw(oPagination.Html)
            End If
        </div>

End If






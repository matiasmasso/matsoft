@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"
    Dim oWebsession As DTO.DTOSession = BLL.BLLSession.Find(Session("SessionId"))
    Dim oUser As DTO.DTOUser = oWebsession.User
    Dim oProduct As New DTO.DTOProductCategory(New Guid("C6D30A46-089E-43DB-8DA5-00D67EF0D28E")) 'Trilogy Colors
    Dim items As Dictionary(Of DTO.DTORep, Integer) = BLL.BLLReps.SalePoints(oProduct)
    Dim maxWidth As Integer = items.Max(Function(x) x.Value)
End Code

<div class="pagewrapper">
    <div>
        <img src='http://www.matiasmasso.es/img/41/24148beb-4699-4b0d-8a29-9b8927440c08' alt='Il Redentore' width='100%' style='max-width:600px;' />
    </div>

    <div class="PageTitle">
        Promoción Il Redentore. Ranking de puntos de venta por comercial
    </div>

@For Each item In items
    @<div class="BarRow">
         <div class="ResultBar" style="width:@CInt(100 * item.Value / maxWidth)%">
             &nbsp;
         </div>
         <span>&nbsp; @String.Format("{0:000} {1}", item.Value, item.Key.NickName)</span>
    </div>
Next

    <p>
        Bases:
        </p><p>
        Viaje a Inglesina (Italia) 2 noches para 2 personas con asistencia a los fuegos de Venecia la <a href="http://balichws.com/venice-festa-del-redentore/">noche de Il Redentore</a> para el que consiga un mayor número de puntos de venta de Trilogy Colors hasta el 15 de Junio 2016.
    </p>

</div>

@Section Styles
    <style>
        .BarRow {
            position:relative;
            height:24px;
            padding:4px 0;
        }

        .ResultBar {
            position:absolute;
            top:0px;
            background-color:lightblue;
            border:1px solid lightgray;
            height:24px;
            z-index:-3;

              background: red; /* For browsers that do not support gradients */
              background: -webkit-linear-gradient(left, white 100px, lightblue); /* For Safari 5.1 to 6.0 */
              background: -o-linear-gradient(right, white 100px, lightblue); /* For Opera 11.1 to 12.0 */
              background: -moz-linear-gradient(right, white 100px, lightblue); /* For Firefox 3.6 to 15 */
              background: linear-gradient(to right, white 100px, lightblue); /* Standard syntax */
        }

        #grad {

}

    </style>

End Section

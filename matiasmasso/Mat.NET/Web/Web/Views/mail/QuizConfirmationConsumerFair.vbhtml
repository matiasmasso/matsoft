@ModelType List(Of DTO.DTOQuizConsumerFair)
@Code
    ViewData("Title") = "QuizConfirmationConsumerFair"
    Layout = "~/Views/mail/_Layout.vbhtml"
End Code

    <p>
        Gracias por registrarse para participar en nuestros stands de <a href="https://www.matiasmasso.es/eventos/2015/bebesymamas/barcelona">bebes y mamas</a> de Barcelona, abril 2015.</p>
    <p>
        Confirmamos sus datos, que puede modificar en cualquier momento antes del 15 de Marzo desde el siguiente enlace:
        <br/><a href="https://www.matiasmasso.es/bebesymamas/apuntame">www.matiasmasso.es/bebesymamas/apuntame</a>
    </p>

    @If Model.count = 0 Then
        @<p>
            No consta registrado para participar en ninguno de nuestros stands
        </p>
    Else
        @<div>
             <p>Dispongo de 2 personas y TPV por stand y franja horaria: <b>@IIf(Model(0).Tpv2pax, "SI", "NO")</b></p>
             <table style="font-family: Helvetica Neue, Helvetica, Arial, sans-serif; font-size: 19px; line-height: 1.2em;" cellpadding="0" cellspacing="0">
                 <tr>
                     <td>&nbsp;</td>
                     <td style="text-align:center;">Sábado 11<br />mañana<br />(10:00-15:00)</td>
                     <td style="text-align:center;">Sábado 11<br />tarde<br />(15:00-20:00)</td>
                     <td style="text-align:center;">Domingo 12<br />mañana<br />(10:00-15:00)</td>
                     <td style="text-align:center;">Domingo 12<br />tarde<br />(15:00-20:00)</td>
                 </tr>

                 <tr>
                     <td>Britax Römer</td>
                     <td style="text-align:center;font-weight:700;border:1px solid gray;;border:1px solid gray;">
                         @IIf(Model.Exists(Function(x) x.Brand.Guid.Equals(New Guid("d4c2bc59-046d-42d3-86e3-bdca91fb473f")) And x.Franja = 0), "X", "")
                     </td>
                     <td style="text-align:center;font-weight:700;border:1px solid gray;">
                         @IIf(Model.Exists(Function(x) x.Brand.Guid.Equals(New Guid("d4c2bc59-046d-42d3-86e3-bdca91fb473f")) And x.Franja = 1), "X", "")
                     </td>
                     <td style="text-align:center;font-weight:700;border:1px solid gray;">
                         @IIf(Model.Exists(Function(x) x.Brand.Guid.Equals(New Guid("d4c2bc59-046d-42d3-86e3-bdca91fb473f")) And x.Franja = 2), "X", "")
                     </td>
                     <td style="text-align:center;font-weight:700;border:1px solid gray;">
                         @IIf(Model.Exists(Function(x) x.Brand.Guid.Equals(New Guid("d4c2bc59-046d-42d3-86e3-bdca91fb473f")) And x.Franja = 3), "X", "")
                     </td>
                 </tr>

                 <tr>
                     <td>Inglesina</td>
                     <td style="text-align:center;font-weight:700;border:1px solid gray;">
                         @IIf(Model.Exists(Function(x) x.Brand.Guid.Equals(New Guid("b1a0fb03-0c18-4607-9091-df5a6a635bb0")) And x.Franja = 0), "X", "")
                     </td>
                     <td style="text-align:center;font-weight:700;border:1px solid gray;">
                         @IIf(Model.Exists(Function(x) x.Brand.Guid.Equals(New Guid("b1a0fb03-0c18-4607-9091-df5a6a635bb0")) And x.Franja = 1), "X", "")
                     </td>
                     <td style="text-align:center;font-weight:700;border:1px solid gray;">
                         @IIf(Model.Exists(Function(x) x.Brand.Guid.Equals(New Guid("b1a0fb03-0c18-4607-9091-df5a6a635bb0")) And x.Franja = 2), "X", "")
                     </td>
                     <td style="text-align:center;font-weight:700;border:1px solid gray;">
                         @IIf(Model.Exists(Function(x) x.Brand.Guid.Equals(New Guid("b1a0fb03-0c18-4607-9091-df5a6a635bb0")) And x.Franja = 3), "X", "")
                     </td>
                 </tr>

                 <tr>
                     <td>4moms</td>
                     <td style="text-align:center;font-weight:700;border:1px solid gray;">
                         @IIf(Model.Exists(Function(x) x.Brand.Guid.Equals(New Guid("67058f90-1fd6-4ae6-82ed-78447779b358")) And x.Franja = 0), "X", "")
                     </td>
                     <td style="text-align:center;font-weight:700;border:1px solid gray;">
                         @IIf(Model.Exists(Function(x) x.Brand.Guid.Equals(New Guid("67058f90-1fd6-4ae6-82ed-78447779b358")) And x.Franja = 1), "X", "")
                     </td>
                     <td style="text-align:center;font-weight:700;border:1px solid gray;">
                         @IIf(Model.Exists(Function(x) x.Brand.Guid.Equals(New Guid("67058f90-1fd6-4ae6-82ed-78447779b358")) And x.Franja = 2), "X", "")
                     </td>
                     <td style="text-align:center;font-weight:700;border:1px solid gray;">
                         @IIf(Model.Exists(Function(x) x.Brand.Guid.Equals(New Guid("67058f90-1fd6-4ae6-82ed-78447779b358")) And x.Franja = 3), "X", "")
                     </td>
                 </tr>

                 <tr>
                     <td>Bob</td>
                     <td style="text-align:center;font-weight:700;border:1px solid gray;">
                         @IIf(Model.Exists(Function(x) x.Brand.Guid.Equals(New Guid("63f67fdb-812f-49f9-b06c-023ee8a984ec")) And x.Franja = 0), "X", "")
                     </td>
                     <td style="text-align:center;font-weight:700;border:1px solid gray;">
                         @IIf(Model.Exists(Function(x) x.Brand.Guid.Equals(New Guid("63f67fdb-812f-49f9-b06c-023ee8a984ec")) And x.Franja = 1), "X", "")
                     </td>
                     <td style="text-align:center;font-weight:700;border:1px solid gray;">
                         @IIf(Model.Exists(Function(x) x.Brand.Guid.Equals(New Guid("63f67fdb-812f-49f9-b06c-023ee8a984ec")) And x.Franja = 2), "X", "")
                     </td>
                     <td style="text-align:center;font-weight:700;border:1px solid gray;">
                         @IIf(Model.Exists(Function(x) x.Brand.Guid.Equals(New Guid("63f67fdb-812f-49f9-b06c-023ee8a984ec")) And x.Franja = 3), "X", "")
                     </td>
                 </tr>
             </table>
            <p>&nbsp;</p>
        </div>
    End If
    


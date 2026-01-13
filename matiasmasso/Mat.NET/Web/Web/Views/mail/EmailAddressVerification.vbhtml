@Code
    Layout = "~/Views/Mail/_Layout.vbhtml"
    Dim sEmail As String = ViewBag.email
    Dim sCodi As String = DTOUser.VerificationCode(sEmail)
End Code

<p>
    Este correo se envía para verificar que la dirección de correo electrónico que ha entrado en nuestra página web está correctamente escrita y le pertenece.
</p>
<p>
    Por favor introduzca el siguiente código en la casilla correspondiente:
</p>
<div style="text-align:center;font-size:larger;font-weight:800">
    @sCodi
</div>
<p>
Si no ha entrado su dirección en nuestra página web puede ignorar este mensaje, no almacenaremos su correo.
</p>

@Code
    Layout = "~/Views/Shared/_Layout.vbhtml"

    Dim oUser = Mvc.ContextHelper.FindUserSync()
    Dim exs As New List(Of Exception)
    Dim oCustomers As New List(Of DTOCustomer)
    If (oUser.Rol.Id = DTORol.Ids.CliFull Or oUser.Rol.Id = DTORol.Ids.CliLite) Then
        oCustomers = FEB2.User.GetCustomersSync(oUser, exs)
    End If
    Dim oBrands As List(Of DTOProductBrand) = FEB2.ProductBrands.AllSync(exs, oUser)
End Code

@If oCustomers.Count = 0 Then
    @<div class="pagewrapper NoBrands">
        No nos constan marcas distribuidas por este usuario<br/>
        Si cree que puede tratarse de un error por favor contacte con nuestras oficinas
    </div>
    
    Else

    @<div class="pagewrapper">
        <div class="PageTitle">Logos de Distribuidor Oficial</div>

        <p>
            Como distribuidor oficial de las siguientes marcas comerciales, le invitamos a que utilice los siguientes distintivos en su página web.<br />

            Para ello pinche cada logotipo con el botón derecho del ratón y seleccione:
            <ol>
                <li>"guardar imagen como" para descargar la imagen en su PC</li>
                <li>"copiar dirección de enlace" para enlazar el logotipo</li>
            </ol>

            El enlace lleva un código de verificación que autentifica su página web como distribuidor oficial.<br />
            Al entrar en nuestra página mediante este enlace aparecen exclusivamente sus datos como distribuidor oficial.<br />
            El efecto dura 48 horas aunque entre en nuestra página mediante un enlace distinto.<br />
            Puede cancelarlo antes borrando las cookies del navegador

        </p>

        <div class="logos">
            @For Each item As DTOProductBrand In oBrands
                    If item.LogoDistribuidorOficial IsNot Nothing Then
                    @<a href="@FEB2.ProductBrand.LogoDistribuidorOficialAnchor(item, oCustomers(0))" target="_blank">
                        <img src="@FEB2.ProductBrand.LogoDistribuidorOficialUrl(item)" title="@item.Nom" />
                    </a>
            End If
        Next
        </div>
    </div>
    End If

    @Section Styles
        <style>
            .NoBrands {
                padding:20px 10px;
                color:red;
                font-weight:bolder;
            }
            .logos {
                text-align: center;
            }

                .logos a {
                    display: inline-block;
                    width: 150px;
                    height: 117px;
                }

                    .logos a img {
                        width: 100%;
                    }
        </style>
    End Section

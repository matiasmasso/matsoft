Public Class CliAperturasController
    Inherits _MatController

    Async Function Index() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Select Case MyBase.Authorize({DTORol.Ids.SuperUser,
                                               DTORol.Ids.Admin,
                                               DTORol.Ids.LogisticManager,
                                               DTORol.Ids.Marketing,
                                               DTORol.Ids.Operadora,
                                               DTORol.Ids.SalesManager,
                                               DTORol.Ids.Comercial,
                                               DTORol.Ids.Rep})
            Case AuthResults.success
                Dim oUser = ContextHelper.GetUser
                Dim oModel As DTOCliApertura.Collection = Await FEB2.CliAperturas.All(oUser, exs)
                ViewBag.Title = Mvc.ContextHelper.Tradueix("Aperturas", "Apertures", "Application Requests")

                retval = View("Aperturas", oModel)
            Case AuthResults.login
                retval = LoginOrView("Aperturas")
            Case AuthResults.denied
                retval = MyBase.UnauthorizedView()
        End Select

        Return retval
    End Function


    Async Function Create(data As String) As Threading.Tasks.Task(Of JsonResult)
        Dim retval As JsonResult = Nothing
        Dim exs As New List(Of Exception)

        If data <> Nothing Then 'per Google crawler

            Dim jss As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim oDictionary As Object = jss.Deserialize(Of Object)(data)
            Dim Model As New DTOCliApertura
            With Model
                If oDictionary("Nom") = "" Then
                    exs.Add(New Exception(ContextHelper.Tradueix("Falta el nombre de la persona de contacto", "Falta el nom de la persona de contacte", "Missing contact person name")))
                ElseIf oDictionary("Nom").ToString.Length < 4 Then
                    exs.Add(New Exception(ContextHelper.Tradueix("Por favor complete el nombre de la persona de contacto", "Si us plau completi el nom de la persona de contacte", "Please complete contact person name")))
                Else
                    .Nom = oDictionary("Nom")
                End If
                .RaoSocial = oDictionary("RaoSocial")
                .NomComercial = oDictionary("NomComercial")
                .Nif = oDictionary("NIF")
                .Adr = oDictionary("Adr")

                If oDictionary("Cit") = "" Then
                    exs.Add(New Exception(ContextHelper.Tradueix("Falta la población", "Falta la població", "Missing store location")))
                Else
                    .Cit = oDictionary("Cit")
                End If

                If oDictionary("Zip") = "" Then
                    exs.Add(New Exception(ContextHelper.Tradueix("Falta el código postal", "Falta el codi postal", "Missing zip code")))
                Else
                    .Zip = oDictionary("Zip")
                End If

                Dim sCountry As String = oDictionary("Country")
                Dim oCountry As New DTOCountry(New Guid(sCountry))
                If .Zip > "" Then
                    Dim oLocation = Await FEB2.Location.FromZip(oCountry, .Zip, exs)
                    If oLocation Is Nothing Then
                        .Zona = FEB2.Zona.FromZipSync(exs, oCountry, .Zip)
                    Else
                        .Zona = oLocation.Zona
                    End If
                End If

                .Tel = oDictionary("Tel")

                If oDictionary("Email") = "" Then
                    exs.Add(New Exception(ContextHelper.Tradueix("Falta el correo electrónico", "Falta el correu electronic", "Missing email")))
                Else
                    .Email = oDictionary("Email")
                End If

                .Web = oDictionary("Web")
                .CodSuperficie = oDictionary("CodSuperficie")
                .CodVolumen = oDictionary("CodVolumen")

                If IsNumeric(oDictionary("SharePuericultura")) Then
                    .SharePuericultura = oDictionary("SharePuericultura")
                End If
                .OtherShares = oDictionary("OtherShares")
                .CodSalePoint = oDictionary("CodSalePoint")
                .Associacions = oDictionary("Associacions")
                .CodAntiguedad = oDictionary("CodAntiguedad")
                If IsDate(oDictionary("FchApertura")) Then
                    .FchApertura = oDictionary("FchApertura")
                Else
                    exs.Add(New Exception(ContextHelper.Tradueix("Falta la fecha de apertura", "Falta la data de apertura", "Missing launch date")))
                End If

                .CodExperiencia = oDictionary("CodExperiencia")

                Dim oBrands() As Object = oDictionary("Brands")
                For Each item As Object In oBrands
                    Dim oGuid As New Guid(item.ToString())
                    Dim oBrand As New DTOGuidNom(oGuid)
                    .Brands.Add(oBrand)
                Next

                .OtherBrands = oDictionary("OtherBrands")
                .Obs = oDictionary("Obs")

                If GuidHelper.IsGuid(oDictionary("ContactClass")) Then
                    Dim oGuid As New Guid(oDictionary("ContactClass").ToString())
                    If Not oGuid.Equals(System.Guid.Empty) Then
                        .ContactClass = New DTOContactClass(oGuid)
                    End If
                End If

                .FchCreated = Now
            End With

            Dim myData As Object = Nothing
            If exs.Count > 0 Then
                myData = New With {.result = "KO", .message = ExceptionsHelper.ToFlatString(exs)}
            Else
                If Await FEB2.CliApertura.Update(Model, exs) Then
                    myData = New With {.result = "OK", .message = "", .guid = Model.Guid.ToString}
                Else
                    myData = New With {.result = "KO", .message = ExceptionsHelper.ToFlatString(exs)}
                End If
            End If
            retval = Json(myData, JsonRequestBehavior.AllowGet)
        End If
        Return retval
    End Function

    Function SingleApertura(guid As Nullable(Of Guid)) As ActionResult
        Dim retval As ActionResult = Nothing
        Dim Model As DTOCliApertura = Nothing
        If guid Is Nothing Then

            retval = NovaApertura()
        Else
            Dim exs As New List(Of Exception)
            Model = FEB2.CliApertura.FindSync(guid, exs)
            If Model IsNot Nothing Then
                retval = View("Apertura", Model)
            End If
        End If
        Return retval
    End Function

    Function NovaFarmacia() As ActionResult
        Dim retval As ActionResult = NovaApertura(DTOContactClass.Wellknowns.Farmacia)
        Return retval
    End Function

    Function NovaGuarderia() As ActionResult
        Dim retval As ActionResult = NovaApertura(DTOContactClass.Wellknowns.Guarderia)
        Return retval
    End Function

    Function NouECommerce() As ActionResult
        Dim retval As ActionResult = NovaApertura(DTOContactClass.Wellknowns.Online)
        Return retval
    End Function

    Function NovaApertura(Optional oContactClassCode As DTOContactClass.Wellknowns = DTOContactClass.Wellknowns.NotSet) As ActionResult
        Dim Model As New DTOCliApertura()
        With Model
            .ContactClass = DTOContactClass.Wellknown(oContactClassCode)
        End With
        Dim retval As ActionResult = View("CreateApertura", Model)
        Return retval
    End Function



    Function SingleAbertura(guid As Nullable(Of Guid)) As ActionResult
        'portugues
        ContextHelper.SetLangCookie(DTOLang.POR)
        Dim retval As ActionResult = SingleApertura(guid)
        Return retval
    End Function

    Async Function pageindexchanged(pageindex As Integer, pagesize As Integer, guid As Guid) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim oUser = ContextHelper.GetUser
        Dim items As DTOCliApertura.Collection = Await FEB2.CliAperturas.All(oUser, exs)
        Dim Model As New DTOCliApertura.Collection
        Dim indexFrom As Integer = pageindex * pagesize
        For i As Integer = indexFrom To indexFrom + pagesize - 1
            If i >= items.Count Then Exit For
            Model.Add(items(i))
        Next
        Dim retval As PartialViewResult = PartialView("Aperturas_", Model)
        Return retval
    End Function

    Async Function Location(data As String) As Threading.Tasks.Task(Of JsonResult)
        Dim retval As JsonResult = Nothing
        Dim exs As New List(Of Exception)
        If data <> String.Empty Then
            Dim jss As New System.Web.Script.Serialization.JavaScriptSerializer()
            Dim oDictionary As Object = jss.Deserialize(Of Object)(data)
            Dim sCountry As String = oDictionary("Country")
            Dim sZipCod As String = oDictionary("Zip")
            Dim oLocation As DTOLocation = Nothing
            If GuidHelper.IsGuid(sCountry) Then
                Dim oCountry As New DTOCountry(New Guid(sCountry))
                oLocation = Await FEB2.Location.FromZip(oCountry, sZipCod, exs)
            End If
            Dim myData As Object = Nothing
            If oLocation Is Nothing Then
                myData = New With {.result = "KO"}
            Else
                myData = New With {.result = "OK", .location = oLocation.Nom}
            End If
            retval = Json(myData, JsonRequestBehavior.AllowGet)
        End If
        Return retval
    End Function

    Async Function Update(data As String) As Threading.Tasks.Task(Of JsonResult)
        Dim jss As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim oDictionary As Object = jss.Deserialize(Of Object)(data)
        Dim sGuid As String = oDictionary("Guid")
        Dim oGuid As New Guid(sGuid)
        Dim exs As New List(Of Exception)
        Dim Model = FEB2.CliApertura.FindSync(oGuid, exs)
        With Model
            .RepObs = oDictionary("RepObs")
            .CodTancament = oDictionary("Status")
        End With
        Dim myData As Object = Nothing
        If Await FEB2.CliApertura.Update(Model, exs) Then
            myData = New With {.result = "OK", .message = ""}
        Else
            myData = New With {.result = "KO", .message = ExceptionsHelper.ToFlatString(exs)}
        End If
        Dim retval As JsonResult = Json(myData, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Async Function EmailConfirmation(guid As Guid) As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        Dim oCliApertura As DTOCliApertura = FEB2.CliApertura.FindSync(guid, exs)
        Dim myData As Object
        If Await FEB2.CliApertura.Send(GlobalVariables.Emp, oCliApertura, exs) Then
            myData = New With {.result = "OK"}
        Else
            myData = New With {.result = "KO"}
        End If
        Dim retval As JsonResult = Json(myData, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

End Class

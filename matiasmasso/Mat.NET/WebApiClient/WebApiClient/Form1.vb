Imports Newtonsoft.Json.Linq
Imports Microsoft.VisualBasic
Imports Newtonsoft.Json
Imports System
Imports System.Collections.Generic
Imports BLL


Public Class Form1
    Private Const romer As String = "D4C2BC59-046D-42D3-86E3-BDCA91FB473F"
    Private Const fourMoms As String = "67058F90-1FD6-4AE6-82ED-78447779B358"
    Private Const duoPlus As String = "4BDA2163-7592-4BF8-A120-96E2967668DE"
    Private Const mamaRooSku As String = "FC13FB77-7244-42AD-A86F-2A00DC7CD2FA"
    Private Const spain As String = "AEEA6300-DE1D-4983-9AA2-61B433EE4635"
    Private Const zonaBarcelona As String = "5D799DC5-B56B-4F8D-AA86-BB318EBFB89F"
    Private Const zonaMallorca As String = "1E000EA5-E807-4E27-A752-97CC80709EBB"
    Private Const Sabadell As String = "1FB11B25-7D87-4059-A9E4-24FA2B0DD98F"
    Private Const rabasa As String = "938AA55C-2AD5-451D-A14A-6F5AF1D1A888" 'CLIENT
    Private Const pazmares As String = "CFCFADE1-5447-46F4-9F55-A36BB16E85FB"
    Private Const reprosillo As String = "59A734EE-67D9-4B0D-86E6-94154CAAF733"
    Private Const repCampins As String = "E04D9CB7-B9C8-40DE-AFA4-C3E3FE44BDC1"
    Private Const incidencia As String = "F7A544DC-7196-4E8A-9DFC-2D2B52CD7BC0"
    Private Const testlead As String = "443638D1-89DA-4498-B513-B8CA387BD015"
    Private Const farmacia As String = "DBED099A-F494-42BC-A59E-03908E497F34" 'Customer
    Private Const tommeetippee As String = "B55B006D-3322-4E41-8CF7-9A02C3503A09" 'Brand

    Private UrlProduction As String = "https://matiasmasso-api.azurewebsites.net/"
    Private urlDeveloper As String = "http://localhost:50535/"
    Private _AllowEvents As Boolean

    Private Enum Modes
        HelloWorld
        VendorStocksUpload
        CompactContacts
        CompactPurchaseOrders
        OutVivace
        ApiBasket
        ApiBasketProves
        Stocks
        SetLang
        RepAtlas
        Tarifas
        Cca
        SumasYSaldos
        PromoPurchaseOrders
        Promo
        Promos
        ClassContacts
        ContactClasses
        userUpdate
        nextOrCurrentRaffle
        demo
        UsersSearch
        IncidenciaAttachments
        IncidenciaSprite
        CountriesAndZonas
        Noticia
        Noticias
        Incidencias
        Spvs
        PendingSkus
        PendingOrders
        mailBasket
        VerificationCodeRequest
        validateEmail
        neighbours
        purchaseorder
        PurchaseOrders
        Deliveries
        Invoices
        StatQueryBrands
        StatQueryCategories
        IForgot
        Impagats
        Credencial
        CredencialUpdate
        Mems
        NewMem
        MemUpdate
        Correspondencia
        Bancs
        Contracts
        Brands
        Skus
        Atlas
        Countries
        SpvsNotRead
        SpvIns
        Spv
        Zonas
        Locations
        Contacts
        Vehicles
        Staffs
        Staff
        Reps
        Downloads
        Nomines
        RepZonas
        RepLiqs
        ContactSearch
        Stat
        Tels
        Emails
        Ediversa
        BrandsPerUser
        BrandsPerCustomer
        categories
        SkusPerCustomer
        Login
    End Enum

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        Dim exs As New List(Of Exception)
        If BLLApp.Initialize(DTOEmp.Ids.MatiasMasso, DTOSession.AppTypes.MatNet, DTOLang.Ids.CAT, DTOCur.Ids.EUR, exs) Then

            ' If BLLSession.CreateWindowsSession(exs) Then
            'BLLSession.Current = BLLSession.Find(BLLSession.Current.Guid)
            'Else
            '   Dim oFrm As New Frm_Login
            '  oFrm.ShowDialog()
            'End If

            'GetEmpFromCommandLineArgs()

        Else
            MsgBox("imposible iniciar la aplicació" & vbCrLf & exs(0).Message)
            Me.Close()
            'Application.Exit()
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        With ComboBoxUser
            .DisplayMember = "Nom"
            .Items.Add(New DTOGuidNom(New Guid("961297AF-BC62-44ED-932A-2C445B7D69C3"), "matias"))
            .Items.Add(New DTOGuidNom(New Guid("5FA9EE85-02D2-415A-AB30-A015A240CD13"), "toni"))
            .Items.Add(New DTOGuidNom(New Guid("43291502-BBC6-401D-967A-0C1D707B0C41"), "josep"))
            .Items.Add(New DTOGuidNom(New Guid("0BFC6E6C-1E78-48ED-B105-B16A19869840"), "carlos Ruiz"))
            .Items.Add(New DTOGuidNom(New Guid("A32BDAA6-FCB2-4E6F-B191-0FE09B339DFE"), "manolo"))
            .Items.Add(New DTOGuidNom(New Guid("7ac3b5cd-c0eb-40c3-820b-5d3fe44abf05"), "rosillo"))
            .Items.Add(New DTOGuidNom(New Guid("3F378C02-5CAF-4A18-B4A8-69D0584C8CBF"), "bascompte"))
            .Items.Add(New DTOGuidNom(New Guid("F7480480-57E2-4C20-ABC2-573E2FD3EC9B"), "federico"))
            .Items.Add(New DTOGuidNom(New Guid("E04D9CB7-B9C8-40DE-AFA4-C3E3FE44BDC1"), "repCampins"))
            .Items.Add(New DTOGuidNom(New Guid("11B4D109-802B-4E26-9BED-8FDF4E3AA39E"), "userCampins"))

            'E04D9CB7-B9C8-40DE-AFA4-C3E3FE44BDC1
            .SelectedIndex = 0
        End With
        LoadModes()
        _AllowEvents = True
    End Sub

    Private Function CurrentUser() As DTOGuidNom
        Return ComboBoxUser.SelectedItem
    End Function

    Private Sub LoadModes()
        ListBoxMode.DataSource = [Enum].GetValues(GetType(Modes))
        ListBoxMode.SelectedItem = Nothing
    End Sub

    Private Sub ListBoxMode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBoxMode.SelectedIndexChanged
        Dim startTime As Date = Now
        Dim exs As New List(Of Exception)
        Dim retval As String = ""
        Dim result As Boolean
        If _AllowEvents Then
            Dim oMode As Modes = ListBoxMode.SelectedItem
            Select Case oMode
                Case Modes.HelloWorld
                    result = GetHelloWorld(retval, exs)
                Case Modes.VendorStocksUpload
                    result = GetVendorStocksUpload(retval, exs)
                Case Modes.CompactContacts
                    result = GetCompactContacts(retval, exs)
                Case Modes.CompactPurchaseOrders
                    result = GetCompactPurchaseOrders(retval, exs)
                Case Modes.OutVivace
                    result = GetOutVivace(retval, exs)
                Case Modes.ApiBasket
                    result = GetApiBasket(retval, exs)
                Case Modes.ApiBasketProves
                    result = GetApiBasketProves(retval, exs)
                Case Modes.Stocks
                    result = GetStocks(retval, exs)
                Case Modes.SetLang
                    result = SetLang(retval, exs)
                Case Modes.RepAtlas
                    result = GetRepAtlas(retval, exs)
                Case Modes.Tarifas
                    result = GetTarifas(retval, exs)
                Case Modes.Cca
                    result = GetCca(retval, exs)
                Case Modes.SumasYSaldos
                    result = GetSumasYSaldos(retval, exs)
                Case Modes.PromoPurchaseOrders
                    result = GetPromoPurchaseOrders(retval, exs)
                Case Modes.Promo
                    result = GetPromo(retval, exs)
                Case Modes.Promos
                    result = GetPromos(retval, exs)
                Case Modes.ClassContacts
                    result = GetClassContacts(retval, exs)
                Case Modes.ContactClasses
                    result = GetContactClasses(retval, exs)
                Case Modes.userUpdate
                    result = GetUserUpdate(retval, exs)
                Case Modes.nextOrCurrentRaffle
                    result = GetNextOrCurrentRaffle(retval, exs)
                Case Modes.demo
                    result = GetDemo(retval, exs)
                Case Modes.UsersSearch
                    result = GetUsersSearch(retval, exs)
                Case Modes.IncidenciaAttachments
                    result = GetIncidenciaAttachments(retval, exs)
                Case Modes.IncidenciaSprite
                    result = GetIncidenciaSprite(retval, exs)
                Case Modes.CountriesAndZonas
                    result = GetCountriesAndZonas(retval, exs)
                Case Modes.Noticias
                    result = GetNoticias(retval, exs)
                Case Modes.Incidencias
                    result = GetIncidencias(retval, exs)
                Case Modes.Spvs
                    result = GetSpvs(retval, exs)
                Case Modes.PendingSkus
                    result = GetPendingSkus(retval, exs)
                Case Modes.PendingOrders
                    result = GetPendingOrders(retval, exs)
                Case Modes.mailBasket
                    result = GetMailBasket(retval, exs)
                Case Modes.VerificationCodeRequest
                    result = GetVerificationCodeRequest(retval, exs)
                Case Modes.validateEmail
                    result = GetvalidateEmail(retval, exs)
                Case Modes.neighbours
                    result = GetNeighbours(retval, exs)
                Case Modes.purchaseorder
                    result = GetPurchaseOrder(retval, exs)
                Case Modes.PurchaseOrders
                    result = GetPurchaseOrders(retval, exs)
                Case Modes.Deliveries
                    result = GetDeliveries(retval, exs)
                Case Modes.Invoices
                    result = GetInvoices(retval, exs)
                Case Modes.StatQueryBrands
                    result = GetStatQueryBrands(retval, exs)
                Case Modes.StatQueryCategories
                    result = GetStatQueryCategories(retval, exs)
                Case Modes.IForgot
                    result = GetIForgot(retval, exs)
                Case Modes.Impagats
                    result = GetImpagats(retval, exs)
                Case Modes.Credencial
                    result = NewCredencial(retval, exs)
                Case Modes.CredencialUpdate
                    result = UpdateCredencial(retval, exs)
                Case Modes.Mems
                    result = GetMems(retval, exs)
                Case Modes.NewMem
                    result = NewMem(retval, exs)
                Case Modes.Correspondencia
                    result = GetCorrespondencies(retval, exs)
                Case Modes.Bancs
                    result = GetBancs(retval, exs)
                Case Modes.Contracts
                    result = GetContracts(retval, exs)
                Case Modes.MemUpdate
                    result = MemUpdate(retval, exs)
                Case Modes.Brands
                    result = GetBrands(retval, exs)
                Case Modes.Skus
                    result = GetBrandSkus(retval, exs)
                Case Modes.Noticia
                    result = GetLastNoticia(retval, exs)
                Case Modes.Atlas
                    result = GetAtlas(retval, exs)
                Case Modes.Countries
                    result = GetCountries(retval, exs)
                Case Modes.SpvsNotRead
                    result = GetSpvsNotRead(exs)
                Case Modes.SpvIns
                    result = GetSpvIns(retval, exs)
                Case Modes.Spv
                    result = GetSpv(retval, exs)
                Case Modes.Zonas
                    result = GetZonas(retval, exs)
                Case Modes.Locations
                    result = GetLocations(retval, exs)
                Case Modes.Contacts
                    result = GetContacts(retval, exs)
                Case Modes.Vehicles
                    result = GetVehicles(retval, exs)
                Case Modes.Staffs
                    result = GetStaffs(retval, exs)
                Case Modes.Staff
                    result = GetStaff(retval, exs)
                Case Modes.Reps
                    result = GetReps(retval, exs)
                Case Modes.Downloads
                    result = GetDownloads(retval, exs)
                Case Modes.Nomines
                    result = GetNomines(retval, exs)
                Case Modes.RepZonas
                    result = GetRepZonas(retval, exs)
                Case Modes.RepLiqs
                    result = GetRepLiqs(retval, exs)
                Case Modes.ContactSearch
                    result = SearchContacts(retval, exs)
                Case Modes.Stat
                    result = GetStatQuery(retval, exs)
                Case Modes.Tels
                    result = GetTels(retval, exs)
                Case Modes.Emails
                    result = GetEmails(retval, exs)
                Case Modes.Ediversa
                    result = GetEdiversa(retval, exs)
                Case Modes.BrandsPerUser
                    result = GetBrandsPerUser(retval, exs)
                Case Modes.BrandsPerCustomer
                    result = GetBrandsPerCustomer(retval, exs)
                Case Modes.categories
                    result = GetCategoriesPerContact(retval, exs)
                Case Modes.SkusPerCustomer
                    result = GetSkusPerContact(retval, exs)
                Case Modes.Login
                    result = Login(retval, exs)
            End Select

            If result Then
                PrintResult(startTime, retval)
            Else
                MsgBox(exs(0).Message, MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub



#Region "Procedures"

    Private Function GetHelloWorld(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/helloWorld"

        Dim oJson = New JObject()
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval

    End Function

    Private Function GetVendorStocksUpload(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/vendor/stocks/upload"

        Dim oGlider As New DTOCompactOnlineVendor
        oGlider.Guid = DTOOnlineVendor.Wellknown(DTOOnlineVendor.WellKnowns.Glider).Guid
        BLLOnlineVendor.LoadTestStocks(oGlider)
        Dim sJsonString As String = JsonConvert.SerializeObject(oGlider)

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function
    Private Function GetCompactContacts(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/compact/sellout/contacts"
        'Dim sJsonString As String = "{'guid':'b7867683-c04b-495f-a503-01b9fc878453'}"
        Dim oJson = New JObject()
        oJson.Add("guid", CurrentUser.Guid)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetCompactPurchaseOrders(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/compact/sellout/contacts"
        'Dim sJsonString As String = "{'guid':'b7867683-c04b-495f-a503-01b9fc878453'}"
        Dim oJson = New JObject()
        oJson.Add("guid", CurrentUser.Guid)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetOutVivace(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/vivace/outvivace/pruebas"
        Dim sJsonString As String = "{'fecha':'10-02-2017','remite':{'nif':'A62572342','nombre':'Vivace Logistica'},'destino':{'nif':'A58007857','nombre':'MATIAS MASSO, S.A.'},'expediciones':[{'vivace':'2017/22614','bultos':3,'m3':0.123,'kg':24,'transpnif':'A28815017','albaranes':[{'numero':'2017011792','cargos':[{'codigo':'METCIVR2','unidades':9,'precio':0.09},{'codigo':'EMB4VR2','unidades':1,'precio':0.34}]}],'cargos':[{'codigo':'MMANIP.','unidades':1,'precio':1.68}]},{'vivace':'2017/22615','bultos':2,'m3':0.099,'kg':18,'transpnif':'A28815017','albaranes':[{'numero':'2017011793','cargos':[{'codigo':'METCIVR2','unidades':3,'precio':0.09},{'codigo':'EMB4VR2','unidades':1,'precio':0.34}]}],'cargos':[{'codigo':'MMANIP.','unidades':1,'precio':1.68}]}]}"

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetApiBasket(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/purchaseorder/submit"
        Dim sJsonString As String = "{'user':{'guid':'b7867683-c04b-495f-a503-01b9fc878453'},'customer':{'guid':'54dadcb7-f120-47b1-94f2-9c7f5654bb62'},'items':[{'qty':2,'sku':{'id':22292}}]}"

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetApiBasketProves(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/purchaseorder/submit/pruebas"
        '{"user":{"guid":"b7867683-c04b-495f-a503-01b9fc878453"},"customer":{"guid":"54DADCB7-F120-47B1-94F2-9C7F5654BB62"},"items":[{"qty":2,"sku":22292}]}
        'Dim sJsonString As String = "{'user':{'guid':'b7867683-c04b-495f-a503-01b9fc878453'},'customer':{'guid':'54dadcb7-f120-47b1-94f2-9c7f5654bb62'},'items':[{'qty':2,'sku':{'id':22292}}]}"
        Dim sJsonString As String = "{'user':{'guid':'b7867683-c04b-495f-a503-01b9fc878453'},'customer':{'guid':'54DADCB7-F120-47B1-94F2-9C7F5654BB62},'items':[{'qty':2,'sku':{'id':22292}}]}"

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetStocks(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/stocks"

        Dim oJson = New JObject()
        oJson.Add("guid", CurrentUser.Guid)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval

    End Function

    Private Function SetLang(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/user/lang/update"

        Dim oJson = New JObject()
        oJson.Add("guid", CurrentUser.Guid)
        oJson.Add("lang", CInt(DTOLang.ENG.Id))
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval

    End Function

    Private Function GetRepAtlas(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/rep/atlas"

        Dim oJson = New JObject()
        oJson.Add("guid", CurrentUser.Guid)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval

    End Function
    Private Function GetTarifas(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/tarifas"

        Dim jCustomer = New JObject()
        jCustomer.Add("guid", farmacia)
        Dim jBrand = New JObject
        jBrand.Add("guid", tommeetippee)

        Dim oJson = New JObject()
        oJson.Add("key1", jCustomer)
        oJson.Add("key2", jBrand)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval

    End Function

    Private Function GetCca(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/comptes/cca"

        Dim jUser = New JObject()
        jUser.Add("guid", CurrentUser.Guid.ToString)
        Dim oJson = New JObject()
        oJson.Add("user", jUser)
        oJson.Add("guid", "9D252759-7286-4C85-A1FD-5FE936A615F4")
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval

    End Function
    Private Function GetSumasYSaldos(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/comptes/sumasysaldos"

        Dim jUser = New JObject()
        jUser.Add("guid", CurrentUser.Guid.ToString)
        Dim oJson = New JObject()
        oJson.Add("user", jUser)
        'oJson.Add("fch", Format(Today, "dd/MM/yy"))
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval

    End Function

    Private Function GetPromoPurchaseOrders(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/promo/PurchaseOrders"

        Dim jUser = New JObject()
        jUser.Add("guid", CurrentUser.Guid.ToString)
        Dim jPromo = New JObject()
        jPromo.Add("guid", "E8C87657-FAC7-4EB3-BA8F-840EA4A0E386")
        Dim oJson = New JObject()
        oJson.Add("user", jUser)
        oJson.Add("guidnom", jPromo)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval

    End Function
    Private Function GetPromo(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/promo"

        Dim jUser = New JObject()
        jUser.Add("guid", CurrentUser.Guid.ToString)
        Dim jPromo = New JObject()
        jPromo.Add("guid", "E8C87657-FAC7-4EB3-BA8F-840EA4A0E386")
        Dim oJson = New JObject()
        oJson.Add("user", jUser)
        oJson.Add("guidnom", jPromo)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval

    End Function
    Private Function GetPromos(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/promos/active"

        Dim oJson = New JObject()
        oJson.Add("guid", CurrentUser.Guid.ToString)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval

    End Function

    Private Function GetClassContacts(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/class/contacts"

        Dim jUser = New JObject()
        jUser.Add("guid", CurrentUser.Guid.ToString)

        Dim jClass = New JObject()
        jClass.Add("guid", "2C19ABF8-F424-45DF-8690-09F32778A8DB")

        Dim oJson = New JObject()
        oJson.Add("user", jUser)
        oJson.Add("guidnom", jClass)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval

    End Function

    Private Function GetContactClasses(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/contacts/classes"

        Dim oJson = New JObject()
        oJson.Add("guid", CurrentUser.Guid.ToString)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval

    End Function

    Private Function GetUserUpdate(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/user/update"

        Dim jCountry = New JObject()
        jCountry.Add("guid", "aeea6300-de1d-4983-9aa2-61b433ee4635")

        Dim oJson = New JObject()
        oJson.Add("guid", "443638d1-89da-4498-b513-b8ca387bd015")
        oJson.Add("email", "test@test.es")
        oJson.Add("lang", 2)
        oJson.Add("cognoms", "dos")
        oJson.Add("firstnom", "uno")
        oJson.Add("childrenCount", 0)
        oJson.Add("country", jCountry)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval

    End Function

    Private Function GetNextOrCurrentRaffle(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/raffles/currentOrNext"

        Dim oJson = New JObject()
        oJson.Add("guid", CurrentUser.Guid)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval

    End Function
    Private Function GetDemo(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/user/demo"

        Dim oJson = New JObject()
        oJson.Add("email", "baudetester@hotmail.com")
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetUsersSearch(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/users/search"

        Dim oJson = New JObject()
        oJson.Add("email", "yahoo")
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetIncidenciaAttachments(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/incidencia/attachments"

        Dim oJson = New JObject()
        oJson.Add("guid", incidencia)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetIncidenciaSprite(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/incidencia/sprite"
        Dim sJsonString As String = "{'guid':'" & incidencia & "'}"

        Dim oImage As Image = Nothing
        If ImageRequest(sUrl, sJsonString, "application/json", "POST", oImage, exs) Then
            PictureBox1.Image = oImage
        End If

        Return True
    End Function

    Private Function GetCountriesAndZonas(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/countries"

        'Dim oJContact As New JObject
        'oJContact.Add("guid", rabasa)

        Dim oJson = New JObject()
        oJson.Add("guid", CurrentUser.Guid.ToString)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetNoticias(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/noticias"

        'Dim oJContact As New JObject
        'oJContact.Add("guid", rabasa)

        Dim oJson = New JObject()
        oJson.Add("guid", CurrentUser.Guid.ToString)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetIncidencias(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/contact/incidencias"

        'Dim oJContact As New JObject
        'oJContact.Add("guid", rabasa)

        Dim oJson = New JObject()
        oJson.Add("guid", rabasa)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetSpvs(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/contact/spvs"

        ' Dim oJContact As New JObject
        ' oJContact.Add("guid", rabasa)

        Dim oJson = New JObject()
        oJson.Add("guid", rabasa)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetPendingSkus(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/contact/purchaseorders/pendingSkus"

        ' Dim oJContact As New JObject
        ' oJContact.Add("guid", rabasa)

        Dim oJson = New JObject()
        oJson.Add("guid", rabasa)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetPendingOrders(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/contact/purchaseorders/pending"

        ' Dim oJContact As New JObject
        ' oJContact.Add("guid", rabasa)

        Dim oJson = New JObject()
        oJson.Add("guid", rabasa)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetStatQueryBrands(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/stats/products"

        Dim oJUser As New JObject
        oJUser.Add("guid", CurrentUser.Guid.ToString)

        Dim oJson = New JObject()
        oJson.Add("user", oJUser)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetDeliveries(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/contact/deliveries"

        Dim oJson = New JObject()
        oJson.Add("guid", rabasa)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    'A9D7A774-3BC1-4427-9734-669E68916FC5

    Private Function GetMailBasket(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/basket/mailConfirmation"

        Dim oJUser As New JObject
        oJUser.Add("guid", CurrentUser.Guid.ToString)

        Dim oJson = New JObject()
        oJson.Add("user", oJUser)
        oJson.Add("guid", "00780E7D-8259-4C4F-9763-69646FA00E42")
        oJson.Add("confirmationEmailCode", 2)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetVerificationCodeRequest(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/user/VerificationCodeRequest"

        Dim oJson = New JObject()
        oJson.Add("emailAddress", "matias@matiasmasso.es")
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetvalidateEmail(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/user/validateEmail"

        Dim oJson = New JObject()
        oJson.Add("emailAddress", "matias@matiasmasso.es")
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetNeighbours(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/nearestNeighbours"
        Dim oJuser = New JObject()
        oJuser.Add("guid", CurrentUser.Guid)

        Dim oJson = New JObject()
        oJson.Add("user", oJuser)
        oJson.Add("latitude", 41.395464)
        oJson.Add("longitude", 2.156181)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetPurchaseOrder(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/purchaseOrder"

        Dim oJson = New JObject()
        oJson.Add("guid", rabasa)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function
    Private Function GetPurchaseOrders(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/contact/purchaseOrders"

        Dim oJson = New JObject()
        oJson.Add("guid", rabasa)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetInvoices(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/contact/invoices"

        Dim oJson = New JObject()
        oJson.Add("guid", rabasa)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetStatQueryCategories(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/stats/products"

        Dim oJUser As New JObject
        oJUser.Add("Guid", CurrentUser.Guid)

        Dim oJson = New JObject()
        oJson.Add("user", oJUser)
        oJson.Add("filterProduct", New Guid(romer))
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetIForgot(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/user/iforgot"

        Dim oJson = New JObject()
        oJson.Add("email", "matias@matiasmasso.es")
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetImpagats(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/impagats/summary"

        Dim oJson = New JObject()
        oJson.Add("Guid", CurrentUser.Guid.ToString)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function


    Private Function UpdateCredencial(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/credencial/update"
        Dim oCredencial As New DUI.Credencial
        With oCredencial
            .Guid = Guid.NewGuid.ToString
            .Usuari = "usuari"
            .Password = "password"
            .Referencia = "referencia"
            .Url = "url"
            .Obs = "Obs"
        End With

        Dim oUserObj = New JObject()
        oUserObj.Add("Guid", CurrentUser.Guid.ToString)

        Dim oCredencialObj = New JObject
        oCredencialObj.Add("Guid", "")
        oCredencialObj.Add("referencia", "test referencia")
        oCredencialObj.Add("usuari", "usuari")
        oCredencialObj.Add("Url", "Url")
        oCredencialObj.Add("Password", "Password")
        oCredencialObj.Add("Obs", "Obs")

        Dim oJson = New JObject
        oJson.Add("User", oUserObj)
        oJson.Add("Credencial", oCredencialObj)

        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function NewCredencial(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/credencial"
        Dim oGuid As New Guid("F96F4659-F1F3-44AD-BF58-07E864EAA4E6")
        'Dim sJsonString As String = "{'val':{'Guid':'" & oGuid.ToString & "'}}"
        Dim sJsonString As String = "{'user':{'Guid':'" & CurrentUser.Guid.ToString & "'}, 'credencial':{'guid':'" & oGuid.ToString & "'}}"
        ' Dim sJsonString As String = "{'user':{'guid':'" & CurrentUser.Guid.ToString & "'}, 'contact':{'guid':'" & rabasa & "'}}"

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function NewMem(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/mem/update"
        Dim sJsonString As String = "{'user':'" & CurrentUser.Guid.ToString & "', 'contact':'" & rabasa & "', 'text':'esto es un report de prueba a eliminar'}"

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetMems(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/mems"
        Dim sJsonString As String = "{'user':{'guid':'" & CurrentUser.Guid.ToString & "'}, 'contact':{'guid':'" & rabasa & "'}}"

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetReps(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/reps"
        Dim sJsonString As String = "{'user':{'guid':'" & CurrentUser.Guid.ToString & "'}}"

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function


    Private Function GetStaffs(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/staffs"

        Dim oUser As DTOGuidNom = CurrentUser()
        Dim oJson = New JObject()
        oJson.Add("Guid", oUser.Guid)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetStaff(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/staff"

        Dim oStaff As New DUI.Staff
        oStaff.Guid = New Guid("4A941105-6FDC-44C2-B2A4-267F050C41A1") 'Josep

        Dim oUserObj = New JObject()
        oUserObj.Add("Guid", CurrentUser.Guid.ToString)

        Dim oStaffObj = New JObject
        oStaffObj.Add("Guid", oStaff.Guid)

        Dim oJson = New JObject
        oJson.Add("Staff", oStaffObj)
        oJson.Add("User", oUserObj)

        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetVehicles(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/vehicles"
        Dim oJson = New JObject()
        oJson.Add("Guid", CurrentUser.Guid.ToString)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetDownloads(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/downloads"

        Dim sPanamera As String = "dbc78b41-c245-49d4-8b8b-deea186ee148"
        Dim oJson = New JObject()
        oJson.Add("Guid", New Guid(sPanamera).ToString)

        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetNomines(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/nominas"

        Dim sCelia As String = "7D54805C-DE32-4F2E-979E-B36486C07BEC"
        Dim oJson = New JObject()
        oJson.Add("Guid", New Guid(sCelia).ToString)

        'Dim sPanamera As String = "dbc78b41-c245-49d4-8b8b-deea186ee148"
        'Dim oJson = New JObject()
        'oJson.Add("Guid", New Guid(sPanamera).ToString)

        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetRepZonas(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/rep/zonas2"

        Dim oJson = New JObject()
        Dim oRep As New DTORep(CurrentUser.Guid)
        oJson.Add("Guid", oRep.Guid.ToString)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetRepLiqs(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/rep/repliqs"

        Dim oJson = New JObject()
        oJson.Add("Guid", New Guid(reprosillo).ToString)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function SearchContacts(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/contacts/search"

        Dim oUserObj = New JObject()
        oUserObj.Add("Guid", CurrentUser.Guid.ToString)

        Dim oJson = New JObject()
        oJson.Add("user", oUserObj)
        oJson.Add("searchkey", "Mobintex")
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetStatQuery(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/stats"

        Dim oJson = New JObject()
        oJson.Add("user", CurrentUser.Guid.ToString)
        oJson.Add("reportmode", "0")
        oJson.Add("keyCod", CInt(DTOStatQuery.keyCods.years).ToString)
        oJson.Add("valueCod", "0")
        'oJson.Add("filterYear", "2016")
        'oJson.Add("filterMonth", "7")
        'oJson.Add("filterDay", "8")
        oJson.Add("filterClient", rabasa)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetTels(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/contact/tels"

        Dim oJson = New JObject()
        oJson.Add("guid", rabasa)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetEmails(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/contact/emails"

        Dim oJson = New JObject()
        oJson.Add("guid", rabasa)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetEdiversa(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/ediversa/readpending"

        Dim oJson = New JObject()
        'oJson.Add("guid", rabasa)
        Dim sJsonString As String = "" ' oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetBrandsPerUser(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/brands"

        Dim oJson = New JObject()
        oJson.Add("guid", CurrentUser.Guid.ToString)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetBrandsPerCustomer(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/contact/brands"

        Dim oJson = New JObject()
        oJson.Add("guid", farmacia)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetCategoriesPerUser(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/categories"

        Dim oJson = New JObject()
        oJson.Add("Guid", (New Guid(romer)).ToString)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetCategoriesPerContact(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/categories"

        Dim oJContact As New JObject
        oJContact.Add("Guid", rabasa)

        Dim oJProduct As New JObject
        oJProduct.Add("Guid", fourMoms)

        Dim oJson = New JObject()
        oJson.Add("contact", oJContact)
        oJson.Add("product", oJProduct)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetSkusPerContact(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/contact/skus"

        Dim oJContact As New JObject
        oJContact.Add("Guid", pazmares)

        Dim oJProduct As New JObject
        oJProduct.Add("Guid", duoPlus)

        Dim oJson = New JObject()
        'oJson.Add("contact", oJContact)
        oJson.Add("product", oJProduct)
        Dim sJsonString As String = oJson.ToString

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function MemUpdate(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/mem/update"
        Dim sJsonString As String = "{'user':'" & CurrentUser.Guid.ToString & "', 'contact':'" & rabasa & "', 'text':'memo de prova a eliminar'}"

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetContracts(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/contracts/categories"
        Dim sJsonString As String = "{'guid':'" & CurrentUser.Guid.ToString & "'}"

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function


    Private Function GetBancs(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/bancs"
        Dim sJsonString As String = "{'guid':'" & CurrentUser.Guid.ToString & "'}"
        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)

        Dim duiBancs As DUI.Banc() = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DUI.Banc())(jsonOutputString)

        Dim oSpriteRequest As New DUI.Sprite
        oSpriteRequest.itemWidth = 48
        oSpriteRequest.guids = New List(Of Guid)
        For Each item As DUI.Banc In duiBancs
            oSpriteRequest.guids.Add(New Guid(item.Guid))
        Next
        sJsonString = JsonConvert.SerializeObject(oSpriteRequest)

        sUrl = "http://localhost:18734/api/bancs/sprite"
        Dim oImage As Image = Nothing
        If ImageRequest(sUrl, sJsonString, "application/json", "POST", oImage, exs) Then
            PictureBox1.Image = oImage
        End If

        Return retval
    End Function

    Private Function GetCorrespondencies(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/correspondencies"
        Dim sJsonString As String = "{'user':{'guid':'" & CurrentUser.Guid.ToString & "'}, 'contact':{'guid':'" & rabasa & "'}}"

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function Login(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/user/login"

        Dim oJson = New JObject()
        oJson.Add("email", "test@test.es")
        oJson.Add("password", "783961")
        oJson.Add("lang", 3)
        Dim sJsonString As String = oJson.ToString


        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetBrands(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/productbrands"
        Dim sJsonString As String = "{'guid':'" & CurrentUser.Guid.ToString & "'}"

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetBrandSkus(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/productbrand/skus"
        Dim sJsonString As String = "{'brand':{'guid':'" & romer & "'},'user':{'guid':'" & CurrentUser.Guid.ToString & "'}}"

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetSprite(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/products/sprite"
        Dim sJsonString As String = "{'guids':['" & mamaRooSku & "'],'itemWidth':'180'}"

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetAtlas(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/atlas"
        Dim sJsonString As String = "{'guid':'" & CurrentUser.Guid.ToString & "'}"

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetCountries(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/countries"
        Dim sJsonString As String = "{'guid':'" & CurrentUser.Guid.ToString & "'}"

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetSpvsNotRead(exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/spvs/SpvsNotRead"
        Dim sJsonString As String = "{}"

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", sJsonString, exs)
        Return retval
    End Function
    Private Function GetSpvIns(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/spvs/ArrivalPending"
        Dim sJsonString As String = "{'guid':'" & CurrentUser.Guid.ToString & "'}"

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetSpv(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim oSpv As New DTOSpv
        With oSpv
            .Id = 531
            .FchAvis = New Date(2017, 1, 1)
        End With

        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/spv/FromNum"
        Dim sJsonString As String = "{'FchAvis':'" & oSpv.FchAvis.ToString(System.Globalization.CultureInfo.InvariantCulture) & "', 'Id':'" & oSpv.Id & "' }"

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetZonas(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/zonas"
        Dim sJsonString As String = "{'user':{'guid':'" & CurrentUser.Guid.ToString & "'}, 'country':{'guid':'" & spain & "'}}"

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetLocations(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/locations"
        Dim sJsonString As String = "{'user':{'guid':'" & CurrentUser.Guid.ToString & "'}, 'zona':{'guid':'" & zonaMallorca & "'}}"

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function
    Private Function GetContacts(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/contacts"
        Dim sJsonString As String = "{'user':{'guid':'" & CurrentUser.Guid.ToString & "'}, 'location':{'guid':'" & Sabadell & "'}}"

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function

    Private Function GetLastNoticia(ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim sUrl As String = IIf(CheckBoxProduction.Checked, UrlProduction, urlDeveloper) & "api/noticias/current"
        Dim sJsonString As String = "{'guid':'" & CurrentUser.Guid.ToString & "'}"

        Dim retval = SendRequest(sUrl, sJsonString, "application/json", "POST", jsonOutputString, exs)
        Return retval
    End Function
#End Region





    Private Sub PrintResult(startTime As Date, retval As String)
        Dim endTime As DateTime = Now
        Dim ts As TimeSpan = endTime.Subtract(startTime)
        If Convert.ToInt32(ts.Milliseconds) >= 0 Then
            Dim oBytes As Byte() = System.Text.Encoding.Unicode.GetBytes(retval)

            TextBoxTimespan.Text = String.Format("{0:N0} milisegons {1:N0} Kb", Convert.ToInt32(ts.Milliseconds), oBytes.Length / 1000)
        Else
            TextBoxTimespan.Text = "Invalid Input"
        End If

        TextBox1.Text = retval
    End Sub

    Private Function SendRequest(url As String, jsonInputString As String, contentType As String, method As String, ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim uri As New Uri(url)
        Dim jsonDataBytes As Byte() = System.Text.Encoding.UTF8.GetBytes(jsonInputString)
        Dim req As System.Net.WebRequest = System.Net.WebRequest.Create(uri)
        req.ContentType = contentType
        req.Method = method
        req.ContentLength = jsonDataBytes.Length

        Try
            Dim stream = req.GetRequestStream()
            If jsonDataBytes IsNot Nothing Then
                stream.Write(jsonDataBytes, 0, jsonDataBytes.Length)
            End If
            stream.Close()

            Dim oResponse As Net.WebResponse = req.GetResponse()
            Dim oResponseStream = req.GetResponse().GetResponseStream()

            Dim reader As New System.IO.StreamReader(oResponseStream)
            jsonOutputString = reader.ReadToEnd()
            reader.Close()
            oResponseStream.Close()
            retval = True
        Catch ex As Exception
            exs.Add(ex)
        End Try

        Return retval
    End Function

    Private Function ImageRequest(url As String, jsonInputString As String, contentType As String, method As String, ByRef OutputImage As Image, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim jsonDataBytes = System.Text.Encoding.UTF8.GetBytes(jsonInputString)
        Dim uri As New Uri(url)
        Dim req As System.Net.WebRequest = System.Net.WebRequest.Create(uri)
        req.ContentType = contentType
        req.Method = method
        req.ContentLength = jsonDataBytes.Length


        Dim stream = req.GetRequestStream()
        stream.Write(jsonDataBytes, 0, jsonDataBytes.Length)
        stream.Close()

        Try
            Dim oResponse As Net.WebResponse = req.GetResponse()
            Dim oResponseStream = req.GetResponse().GetResponseStream()
            OutputImage = Image.FromStream(oResponseStream)
            oResponseStream.Close()
            retval = True
        Catch ex As Exception
            exs.Add(ex)
        End Try

        Return retval
    End Function

    Private Function MatApiClient() As System.Net.Http.HttpClient
        Dim retval As New System.Net.Http.HttpClient()
        retval.BaseAddress = New Uri("https://microsoft-apiappb7524e42f0554edbb1a212cfc00369be.azurewebsites.net/")
        retval.DefaultRequestHeaders.Accept.Add(New System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"))
        Return retval
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Prueba()
    End Sub
End Class

Public Class DTOIncidencia
    Inherits DTOBaseGuidNumFch

    Property Src As Srcs
    Property Codi As DTOIncidenciaCod

    Property Customer As DTOCustomer
    Property ContactType As ContactTypes
    Property CustomerNameAddress As String
    Property ContactPerson As String
    Property EmailAdr As String
    Property Tel As String
    Property CustomerRef As String
    Property Procedencia As Procedencias
    Property FchCompra As Nullable(Of Date)
    Property Product As Object
    Property SerialNumber As String
    Property Description As String

    Property DocFileImages As List(Of DTODocFile)
    Property ExistImages As Boolean
    Property PurchaseTickets As List(Of DTODocFile)
    Property ExistTickets As Boolean

    Property Catalog As DTOProductCatalog
    Property BrandGuid As Guid
    Property CategoryGuid As Guid
    Property SkuGuid As Guid
    Property AcceptedConditions As Boolean

    Property Spv As DTOSpv
    Property FchClose As Date
    Property Tancament As DTOIncidenciaCod
    Property UsrLog As DTOUsrLog

    Property Url As String

    Public Enum Srcs
        NotSet
        Producte
        Transport
    End Enum

    Public Enum ContactTypes
        NotSet
        Professional
        Consumidor
    End Enum

    Public Enum AttachmentCods
        Ticket
        Imatge
    End Enum

    Public Enum Procedencias
        NotSet
        MyShop
        OtherShops
        Expo
    End Enum

    Public Sub New()
        MyBase.New()
        MyBase.Fch = Now
        _DocFileImages = New List(Of DTODocFile)
        _PurchaseTickets = New List(Of DTODocFile)
        _UsrLog = New DTOUsrLog
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _DocFileImages = New List(Of DTODocFile)
        _PurchaseTickets = New List(Of DTODocFile)
        _UsrLog = New DTOUsrLog
    End Sub

    Shared Function Factory(oContactType As DTOIncidencia.ContactTypes, Optional oSrc As DTOIncidencia.Srcs = DTOIncidencia.Srcs.Producte) As DTOIncidencia
        Dim retval As New DTOIncidencia()
        With retval
            .ContactType = oContactType
            .Src = oSrc
        End With
        Return retval
    End Function

    Shared Function Factory(oCustomer As DTOCustomer, oSrc As DTOIncidencia.Srcs) As DTOIncidencia
        Dim retval = DTOIncidencia.Factory(DTOIncidencia.ContactTypes.Professional, oSrc)
        With retval
            If oCustomer IsNot Nothing Then
                .Customer = oCustomer
                .CustomerNameAddress = DTOContact.HtmlNameAndAddress(oCustomer)
            End If
            .DocFileImages = New List(Of DTODocFile)
            .PurchaseTickets = New List(Of DTODocFile)
            .Fch = Now
        End With
        Return retval
    End Function

    Public Function GetProduct() As DTOProduct
        Dim retval As DTOProduct = Nothing
        If _Product IsNot Nothing Then
            If _Product.GetType().IsSubclassOf(GetType(DTOProduct)) Then
                retval = _Product
            ElseIf TypeOf _product Is DTOProduct Then
                retval = _Product
            Else
                Dim oProduct As DTOProduct = _Product.toobject(Of DTOProduct)
                Select Case oProduct.SourceCod
                    Case DTOProduct.SourceCods.SKU
                        retval = _Product.toobject(Of DTOProductSku)
                    Case DTOProduct.SourceCods.Category
                        retval = _Product.toobject(Of DTOProductCategory)
                    Case DTOProduct.SourceCods.Brand
                        retval = _Product.toobject(Of DTOProductBrand)
                End Select
            End If
        End If
        Return retval
    End Function

    Public Function Attachments() As List(Of DTODocFile)
        Dim retval As New List(Of DTODocFile)
        retval.AddRange(_PurchaseTickets)
        retval.AddRange(_DocFileImages)
        Return retval
    End Function

    Shared Function CodiNom(oCod As DTOIncidenciaCod, oLang As DTOLang) As String
        Dim retval As String = ""
        If oCod IsNot Nothing Then
            retval = oCod.Nom.tradueix(oLang)
        End If
        Return retval
    End Function

    Shared Function MultilineText(oIncidencia As DTOIncidencia) As String
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("Incidencia #" & oIncidencia.Num & " del " & Format(oIncidencia.Fch, "dd/MM/yy"))
        sb.AppendLine(oIncidencia.Customer.FullNom)
        sb.AppendLine(oIncidencia.GetProduct.FullNom())
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Public Shadows Function UrlSegment() As String
        Return MyBase.UrlSegment("incidencia")
    End Function

    Shared Function ExcelReposicions(items As List(Of DTOIncidencia)) As MatHelperStd.ExcelHelper.Sheet

        Dim retval As New MatHelperStd.ExcelHelper.Sheet
        With retval
            .AddColumn("registre", MatHelperStd.ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("data", MatHelperStd.ExcelHelper.Sheet.NumberFormats.DDMMYY)
            .AddColumn("client", MatHelperStd.ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("marca", MatHelperStd.ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("categoria", MatHelperStd.ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("producte", MatHelperStd.ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("cost", MatHelperStd.ExcelHelper.Sheet.NumberFormats.Euro)
        End With

        For Each item As DTOIncidencia In items
            Dim oRow As MatHelperStd.ExcelHelper.Row = retval.AddRow
            With item
                oRow.AddCell(.num, .UrlSegment)
                oRow.AddCell(.fch)
                oRow.AddCell(.Customer.FullNom)
                oRow.AddCell(DTOProduct.BrandNom(.GetProduct))
                oRow.AddCell(DTOProduct.CategoryNom(.GetProduct))
                oRow.AddCell(DTOProduct.SkuNom(.GetProduct))
                oRow.AddCell(DTOProduct.SkuCostEur(.GetProduct))
            End With
        Next
        Return retval
    End Function

End Class

Public Class DTOIncidenciaQuery
    Property emp as DTOEmp
    Property Lang as DTOLang
    Property Catalog As DTOProductCatalog
    Property Product As DTOProduct
    Property Customer As DTOCustomer
    Property Manufacturer As DTOManufacturer
    Property Src As DTOIncidencia.Srcs
    Property Codi As DTOIncidenciaCod
    Property Tancament As DTOIncidenciaCod
    Property IncludeClosed As Boolean
    Property Year As Integer
    Property result As List(Of DTOIncidencia)
    Property Unauthorized As Boolean

    Shared Function Factory(oUser As DTOUser) As DTOIncidenciaQuery
        Dim retval As New DTOIncidenciaQuery
        With retval
            .emp = oUser.Emp
            .Lang = oUser.Lang
            .Src = DTOIncidencia.Srcs.NotSet
            If oUser.Rol.IsStaff Then
                .IncludeClosed = False
            Else
                .Src = DTOIncidencia.Srcs.Producte
                Select Case oUser.Rol.Id
                    Case DTORol.Ids.CliFull, DTORol.Ids.CliLite
                        .IncludeClosed = False
                        .Customer = New DTOCustomer(oUser.Contact.Guid)
                    Case DTORol.Ids.Manufacturer
                        .IncludeClosed = True
                        .Manufacturer = New DTOManufacturer(oUser.Contact.Guid)
                        .Src = DTOIncidencia.Srcs.Producte
                    Case Else
                        .Unauthorized = True
                End Select
            End If
        End With
        Return retval
    End Function
End Class

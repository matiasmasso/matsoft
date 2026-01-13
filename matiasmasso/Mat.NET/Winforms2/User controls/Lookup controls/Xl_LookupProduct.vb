Imports System.ComponentModel

Public Class Xl_LookupProduct
    Inherits Xl_LookupTextboxButton

    Private _Products As List(Of DTOProduct)
    Private _CustomLookup As Boolean
    Private _SelectionMode As DTOProduct.SelectionModes = DTOProduct.SelectionModes.SelectAny

    <Description("Selection mode"), Category("Data")>
    Property SelectionMode As DTOProduct.SelectionModes
        Get
            Return _SelectionMode
        End Get
        Set(value As DTOProduct.SelectionModes)
            _SelectionMode = value
        End Set
    End Property

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)
    Public Event RequestToLookup(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows ReadOnly Property Products() As List(Of DTOProduct)
        Get
            Return _Products
        End Get
    End Property

    Public Shadows ReadOnly Property Product() As DTOProduct
        Get
            Dim retval As DTOProduct = Nothing
            If _Products IsNot Nothing AndAlso _Products.Count > 0 Then
                retval = _Products.First()
            End If
            Return retval
        End Get
    End Property

    Private Sub refresca()
        If _Products Is Nothing OrElse _Products.Count = 0 Then
            MyBase.Text = ""
        Else
            Dim sb As New Text.StringBuilder
            For Each item In _Products
                If sb.Length > 0 Then sb.Append(",")
                sb.Append(item.FullNom())
            Next
            MyBase.Text = sb.ToString()
        End If
    End Sub

    Public Shadows Sub Load(oProduct As DTOProduct, Optional oSelectionMode As DTOProduct.SelectionModes = DTOProduct.SelectionModes.SelectMany, Optional CustomLookup As Boolean = False)
        _Products = New List(Of DTOProduct)
        _Products.Add(oProduct)
        _SelectionMode = oSelectionMode
        _CustomLookup = CustomLookup
        refresca()
        SetContextMenu()
    End Sub

    Public Shadows Sub Load(oProducts As List(Of DTOProduct), Optional oSelectionMode As DTOProduct.SelectionModes = DTOProduct.SelectionModes.SelectMany, Optional CustomLookup As Boolean = False)
        _Products = oProducts
        _SelectionMode = oSelectionMode
        _CustomLookup = CustomLookup
        refresca()
        SetContextMenu()
    End Sub

    Public Sub Clear()
        Me.Products.Clear()
        refresca()
    End Sub

    Private Async Sub Xl_LookupProduct_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim exs As New List(Of Exception)

        If _CustomLookup Then
            Select Case _SelectionMode
                Case DTOProduct.SelectionModes.SelectAny, DTOProduct.SelectionModes.Selectbrand, DTOProduct.SelectionModes.SelectCategory, DTOProduct.SelectionModes.SelectSku
                    Dim oProduct As DTOProduct = Nothing
                    If _Products.Count > 0 Then oProduct = _Products.First
                    RaiseEvent RequestToLookup(Me, New MatEventArgs(oProduct))
                Case DTOProduct.SelectionModes.SelectMany, DTOProduct.SelectionModes.SelectBrands, DTOProduct.SelectionModes.SelectCategories, DTOProduct.SelectionModes.SelectSkus
                    RaiseEvent RequestToLookup(Me, New MatEventArgs(_Products))
            End Select
        Else
            Dim oProducts As New List(Of DTOProduct)
            If _Products Is Nothing Then
                Dim sGuid As String = UIHelper.GetSetting(DTOSession.Settings.Last_Product_Selected)
                If GuidHelper.IsGuid(sGuid) Then
                    Dim oCache = Await FEB.Cache.Fetch(exs, Current.Session.User)
                    If exs.Count = 0 Then
                        oProducts.Add(oCache.ProductOrSelf(New Guid(sGuid)))
                    End If
                End If
            Else
                oProducts = _Products
            End If

            Dim oFrm As New Frm_Catalog(_SelectionMode, oProducts)
            AddHandler oFrm.OnItemsSelected, AddressOf onProductsSelected
            AddHandler oFrm.OnItemSelected, AddressOf onProductsSelected
            oFrm.Show()
        End If
    End Sub

    Private Sub onProductsSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        Select Case _SelectionMode
            Case DTOProduct.SelectionModes.SelectAny, DTOProduct.SelectionModes.Selectbrand, DTOProduct.SelectionModes.SelectCategory, DTOProduct.SelectionModes.SelectSku
                Dim oProduct As DTOProduct = e.Argument
                _Products = New List(Of DTOProduct)
                _Products.Add(oProduct)
            Case DTOProduct.SelectionModes.SelectMany, DTOProduct.SelectionModes.SelectBrands, DTOProduct.SelectionModes.SelectCategories, DTOProduct.SelectionModes.SelectSkus
                _Products = e.Argument
        End Select
        refresca()
        If _Products IsNot Nothing AndAlso _Products.Count > 0 Then
            UIHelper.SaveSetting(DTOSession.Settings.Last_Product_Selected, _Products.First.Guid.ToString())
        End If
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip

        If _Products IsNot Nothing AndAlso _Products.Count > 0 Then
            Dim oMenu_Product As New Menu_Product(_Products)
            AddHandler oMenu_Product.AfterUpdate, AddressOf refresca
            If oMenu_Product.Range IsNot Nothing Then
                oContextMenu.Items.AddRange(oMenu_Product.Range)
            End If
        End If

        MyBase.TextBox1.ContextMenuStrip = oContextMenu
    End Sub

End Class

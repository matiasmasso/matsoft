Public Class Xl_LookupProduct
    Inherits Xl_LookupTextboxButton

    Private _Product As DTOProduct
    Private _CustomLookup As Boolean
    Private _SelectionMode As DTOProduct.SelectionModes = DTOProduct.SelectionModes.SelectAny

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)
    Public Event RequestToLookup(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property Product() As DTOProduct
        Get
            Return _Product
        End Get
        Set(ByVal value As DTOProduct)
            _Product = value
            refresca()
            SetContextMenu()
        End Set
    End Property

    Private Sub refresca()
        If _Product Is Nothing Then
            MyBase.Text = ""
        Else
            MyBase.Text = _Product.FullNom()
        End If
    End Sub

    Public Shadows Sub Load(oProduct As DTOProduct, Optional oSelectionMode As DTOProduct.SelectionModes = DTOProduct.SelectionModes.SelectAny, Optional CustomLookup As Boolean = False)
        _Product = oProduct
        _SelectionMode = oSelectionMode
        _CustomLookup = CustomLookup
        refresca()
        SetContextMenu()
    End Sub

    Public Sub Clear()
        Me.Product = Nothing
    End Sub

    Private Async Sub Xl_LookupProduct_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim exs As New List(Of Exception)
        If _CustomLookup Then
            RaiseEvent RequestToLookup(Me, New MatEventArgs(_Product))
        Else
            Dim oProduct As DTOProduct = Nothing
            If _Product Is Nothing Then
                Dim sGuid As String = UIHelper.GetSetting(DTOSession.Settings.Last_Product_Selected)
                If GuidHelper.IsGuid(sGuid) Then
                    oProduct = Await FEB2.Product.Find(exs, New Guid(sGuid))
                End If
            Else
                oProduct = _Product
            End If
            Select Case _SelectionMode
                Case DTOProduct.SelectionModes.SelectBrand
                    Dim oFrm As New Frm_ProductBrands(_SelectionMode, oProduct)
                    AddHandler oFrm.OnItemSelected, AddressOf onProductSelected
                    oFrm.Show()
                Case DTOProduct.SelectionModes.SelectCategory
                    Dim oFrm As New Frm_ProductCategories(_SelectionMode, oProduct)
                    AddHandler oFrm.OnItemSelected, AddressOf onProductSelected
                    oFrm.Show()
                Case Else
                    Dim oFrm As New Frm_ProductSkus(_SelectionMode, oProduct)
                    AddHandler oFrm.onItemSelected, AddressOf onProductSelected
                    oFrm.Show()
            End Select
        End If
    End Sub

    Private Sub onProductSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _Product = e.Argument
        MyBase.Text = _Product.FullNom()
        UIHelper.SaveSetting(DTOSession.Settings.Last_Product_Selected, _Product.Guid.ToString())
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip

        If _Product IsNot Nothing Then
            Dim oMenu_Product As New Menu_Product(_Product)
            AddHandler oMenu_Product.AfterUpdate, AddressOf refresca
            If oMenu_Product.Range IsNot Nothing Then
                oContextMenu.Items.AddRange(oMenu_Product.Range)
            End If
        End If

        MyBase.TextBox1.ContextMenuStrip = oContextMenu
    End Sub

End Class

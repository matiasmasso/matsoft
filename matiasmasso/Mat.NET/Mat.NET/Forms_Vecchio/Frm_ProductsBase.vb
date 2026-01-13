Public Class Frm_ProductsBase

    Private _Product As DTOProduct0
    Private _Catalog As ProductCatalog

    Private mAllowEvents As Boolean = False

    Private Enum Levels
        NotSet
        Brand
        Category
        Sku
    End Enum

    Public Event AfterSelect(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oProduct As DTOProduct0)
        MyBase.New()
        InitializeComponent()
        _Product = oProduct
        _Catalog = ProductCatalogLoader.Catalog(BLL.BLLApp.Emp)
        LoadBrands()
    End Sub

    Public ReadOnly Property Product() As DTOProduct0
        Get
            Return _Product
        End Get
    End Property

    Private Sub LoadBrands()
        mAllowEvents = False
        With ListBox1
            .DataSource = _Catalog.Brands
            .ValueMember = "Guid"
            .DisplayMember = "Nom"
        End With
        mAllowEvents = True

    End Sub

    Private Sub LoadCategories()
        Dim oBrand As ProductBrand = CurrentBrand()
        Dim oCategories As List(Of ProductCategory) = Nothing
        If CheckBoxHideObsoletos.Checked Then
            oCategories = New List(Of ProductCategory)
            For Each Itm As ProductCategory In oBrand.Categories.FindAll(Function(x) x.Obsoleto = False)
                oCategories.Add(Itm)
            Next
        Else
            oCategories = oBrand.Categories
        End If

        mAllowEvents = False
        With ListBox2
            .DataSource = oCategories
            .ValueMember = "Guid"
            .DisplayMember = "Nom"
        End With
        mAllowEvents = True
    End Sub

    Private Sub LoadSkus()
        Dim oCategory As ProductCategory = CurrentCategory()
        If CheckBoxHideObsoletos.Checked Then
            'Sql = Sql & "AND OBSOLETO=0 "
        End If

        mAllowEvents = False
        With ListBox3
            .DataSource = oCategory.ProductSkus
            .ValueMember = "Guid"
            .DisplayMember = "NomCurt"
        End With
        mAllowEvents = True
    End Sub

    Private Function CurrentBrand() As ProductBrand
        Dim retval As ProductBrand = ListBox1.SelectedItem
        Return retval
    End Function

    Private Function CurrentCategory() As ProductCategory
        Dim retval As ProductCategory = ListBox2.SelectedItem
        Return retval
    End Function

    Private Function CurrentSku() As ProductSku
        Dim retval As ProductSku = ListBox3.SelectedItem
        Return retval
    End Function

    Private Sub ListBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox1.DoubleClick
        _Product = New DTOProduct0(CurrentBrand)
        RaiseEvent AfterSelect(_Product, EventArgs.Empty)
        Me.Close()
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        If mAllowEvents Then
            LoadCategories()
            LoadSkus()
        End If
    End Sub

    Private Sub ListBox2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox2.DoubleClick
        _Product = New DTOProduct0(CurrentCategory)
        RaiseEvent AfterSelect(_Product, EventArgs.Empty)
        Me.Close()
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox2.SelectedIndexChanged
        If mAllowEvents Then
            LoadSkus()
        End If
    End Sub

    Private Sub ListBox3_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox3.DoubleClick
        _Product = New DTOProduct0(CurrentSku)
        RaiseEvent AfterSelect(_Product, EventArgs.Empty)
        Me.Close()
    End Sub



    Private Sub CheckBoxHideObsoletos_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxHideObsoletos.CheckedChanged
        If mAllowEvents Then
            LoadBrands()
        End If
    End Sub
End Class
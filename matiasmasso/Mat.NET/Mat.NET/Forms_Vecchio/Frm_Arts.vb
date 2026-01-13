
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class Frm_Arts
    Private _AllowEvents As Boolean
    Private mLastMouseDownRectangle As System.Drawing.Rectangle
    Private _SelMode As SelModes
    Private _DefaultProduct As Product

    Public Event AfterSelect(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Enum SelModes
        NotSet
        Browse
        SelectProduct
        SelectTpa
        SelectStp
        SelectArt
    End Enum

    Public Sub New(ByVal oSelMode As SelModes, Optional oDefaultProduct As Product = Nothing)
        MyBase.New()
        Me.InitializeComponent()
        _SelMode = oSelMode
        _DefaultProduct = oDefaultProduct


    End Sub

    Private Sub Frm_Arts_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim Fch1 As Date = Now
        LoadMgzs()

        Dim oBrands As List(Of DTOProductBrand) = BLL.BLLProductBrands.All(BLL.BLLApp.Emp, True)
        Dim oDefaultBrand As DTOProductBrand = Nothing
        If _DefaultProduct IsNot Nothing Then
            oDefaultBrand = oBrands.Find(Function(x) x.Guid.Equals(_DefaultProduct.Tpa.Guid))
        End If

        Select Case _SelMode
            Case SelModes.SelectProduct
                Xl_ProductBrands1.Load(oBrands, BLL.Defaults.SelectionModes.Selection, , oDefaultBrand)
                'Xl_ProductCategories1. = BLL.Defaults.SelectionModes.Selection
                Xl_StpArts1.SelectionMode = BLL.Defaults.SelectionModes.Selection
            Case SelModes.SelectTpa
                Xl_ProductBrands1.Load(oBrands, BLL.Defaults.SelectionModes.Selection, , oDefaultBrand)
                'Xl_TpaStps1.SelectionMode = BLL.Defaults.SelectionModes.Browse
                Xl_StpArts1.SelectionMode = BLL.Defaults.SelectionModes.Browse
            Case SelModes.SelectStp
                Xl_ProductBrands1.Load(oBrands, BLL.Defaults.SelectionModes.Browse, , oDefaultBrand)
                'Xl_TpaStps1.SelectionMode = BLL.Defaults.SelectionModes.Selection
                Xl_StpArts1.SelectionMode = BLL.Defaults.SelectionModes.Browse
            Case SelModes.SelectArt
                Xl_ProductBrands1.Load(oBrands, BLL.Defaults.SelectionModes.Browse, , oDefaultBrand)
                'Xl_TpaStps1.SelectionMode = BLL.Defaults.SelectionModes.Browse
                Xl_StpArts1.SelectionMode = BLL.Defaults.SelectionModes.Selection
            Case Else
                Xl_ProductBrands1.Load(oBrands, BLL.Defaults.SelectionModes.Browse, , oDefaultBrand)
                'Xl_TpaStps1.SelectionMode = BLL.Defaults.SelectionModes.Browse
                Xl_StpArts1.SelectionMode = BLL.Defaults.SelectionModes.Browse
        End Select

        LoadStps()
        LoadArts()
        _AllowEvents = True
    End Sub

    Public Sub LoadStps()
        Dim oBrand As DTOProductBrand = Xl_ProductBrands1.Value
        Dim oCategories As List(Of DTOProductCategory) = BLL.BLLProductCategories.All(oBrand, True)
        Dim oSelMode As BLL.Defaults.SelectionModes = BLL.Defaults.SelectionModes.Browse
        Select Case _SelMode
            Case SelModes.SelectStp, SelModes.SelectProduct
                oSelMode = BLL.Defaults.SelectionModes.Selection
        End Select
        Xl_ProductCategories1.Load(oCategories, oSelMode)
    End Sub

    Public Sub LoadArts()
        Dim oStp As Stp = CurrentStp()
        Dim oMgz As New Mgz(CurrentMgz.Guid)
        If oStp IsNot Nothing Then
            Xl_StpArts1.LoadControl(oStp, oMgz)
            ShowArtSubPanel(CurrentArt)
        End If
    End Sub

    Public ReadOnly Property Art() As Art
        Get
            Return CurrentArt()
        End Get
    End Property

    Private Function CurrentBrand() As DTOProductBrand
        Dim retval As DTOProductBrand = Xl_ProductBrands1.Value
        Return retval
    End Function

    Private Function CurrentStp() As Stp
        Dim retval As Stp = New Stp(Xl_ProductCategories1.Value.Guid)
        Return retval
    End Function

    Private Function CurrentArt() As Art
        Dim retval As Art = Xl_StpArts1.Art
        Return retval
    End Function

    Private Function CurrentMgz() As DTOMgz
        Dim retval As DTOMgz = ToolStripComboBoxMgz.ComboBox.SelectedItem
        Return retval
    End Function


    Private Sub Xl_ProductBrands1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_ProductBrands1.ValueChanged
        If _AllowEvents Then
            LoadStps()
            LoadArts()
        End If
    End Sub

    Private Sub Xl_ProductCategories1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_ProductCategories1.ValueChanged
        LoadArts()
    End Sub

    Private Sub Xl_StpArts1_SelectionChanged(sender As Object, e As MatEventArgs) Handles Xl_StpArts1.SelectionChanged
        If _AllowEvents Then
            ShowArtSubPanel(e.Argument)
        End If
    End Sub


    Private Sub Do_Excel(ByVal sender As Object, ByVal e As System.EventArgs)
        Xl_StpArts1.Excel.Visible = True
    End Sub

    Private Sub ShowArtSubPanel(Optional ByVal oArt As Art = Nothing)
        If oArt Is Nothing Then
            PictureBoxArt.Image = Nothing
            TextBoxPack.Clear()
            'TextBoxTarifaA.Clear()
            TextBoxPvp.Clear()
            LabelMyd.Text = ""
            PictureBoxForzarInnerPack.Visible = False
        Else
            PictureBoxArt.Image = oArt.Image
            If oArt.Dimensions IsNot Nothing Then
                TextBoxPack.Text = oArt.Dimensions.SelfOrInherited.InnerPack
                PictureBoxForzarInnerPack.Visible = oArt.Dimensions.SelfOrInherited.ForzarInnerPack
            End If
            'If oArt.TarifaA Is Nothing Then
            'TextBoxTarifaA.Clear()
            'Else
            'TextBoxTarifaA.Text = oArt.TarifaA.CurFormat
            'End If
            If oArt.Pvp Is Nothing Then
                TextBoxPvp.Clear()
            Else
                TextBoxPvp.Text = oArt.Pvp.CurFormat
            End If
            LabelMyd.Text = oArt.Nom_ESP
        End If
    End Sub


    Private Sub ToolStripButtonRefresca_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonRefresca.Click
        LoadArts()
    End Sub

    Private Sub ToolStripButtonBusca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonBusca.Click
        root.ShowSelArt()
    End Sub

    Private Sub ToolStripButtonIncentius_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonIncentius.Click
        Dim oFrm As New Frm_Incentius
        oFrm.Show()
    End Sub

    Private Sub ToolStripButtonCdImg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonCdImg.Click
        ShowCdMake()
    End Sub

    Private Sub ToolStripButtonPromos1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonPromos.Click
        Dim oFrm As New Frm_Art_Promos(Frm_Art_Promos.Modes.Consulta)
        oFrm.Show()
    End Sub

    Private Sub Do_ExcelAllLogos()
        Dim oApp As New Excel.Application()
        'oApp.UserControl = True
        Dim oldCI As System.Globalization.CultureInfo = _
            System.Threading.Thread.CurrentThread.CurrentCulture
        System.Threading.Thread.CurrentThread.CurrentCulture = _
            New System.Globalization.CultureInfo("en-US")


        Dim oWb As Excel.Workbook = oApp.Workbooks.Add()
        Dim oSheet As Excel.Worksheet = oWb.ActiveSheet

        Dim i As Integer
        For Each oTpa As Tpa In App.Current.Emp.Tpas(False)
            If oTpa.LogoVectorial IsNot Nothing Then
                i += 1

                Dim sTxt As String = oTpa.Nom
                Dim sUrl As String = oTpa.LogoVectorialUrl(True)
                Dim oRange As Excel.Range = oSheet.Cells(i, 1)
                oSheet.Hyperlinks.Add(oRange, sUrl, , , sTxt)

            End If

        Next


        oApp.Visible = True

        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI

    End Sub


    Private Sub Do_ExcelAllEBooks()
        Dim oApp As New Excel.Application()
        'oApp.UserControl = True
        Dim oldCI As System.Globalization.CultureInfo = _
            System.Threading.Thread.CurrentThread.CurrentCulture
        System.Threading.Thread.CurrentThread.CurrentCulture = _
            New System.Globalization.CultureInfo("en-US")


        Dim oWb As Excel.Workbook = oApp.Workbooks.Add()
        Dim oSheet As Excel.Worksheet = oWb.ActiveSheet

        Dim i As Integer
        For Each oTpa As Tpa In App.Current.Emp.Tpas(False)
            i += 1

            Dim sTxt As String = oTpa.Nom
            Dim sUrl As String = oTpa.ePubBookUrl(True)
            Dim oRange As Excel.Range = oSheet.Cells(i, 1)
            oSheet.Hyperlinks.Add(oRange, sUrl, , , sTxt)

        Next


        oApp.Visible = True

        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
    End Sub

    Private Function isDraggingImage(ByVal e As System.Windows.Forms.DragEventArgs) As Boolean
        Dim retval As Boolean = False
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            Dim sFilenames() As String = CType(e.Data.GetData(DataFormats.FileDrop), String())
            If sFilenames.Length > 0 Then
                Dim sFilename As String = sFilenames(0)
                If BLL.IsImage(sFilename) Then
                    retval = True
                End If
            End If
        End If
        Return retval
    End Function

    Private Sub LoadMgzs()
        Dim oEmp as DTOEmp = BLL.BLLApp.Emp
        Dim oMgzs As List(Of DTOMgz) = BLL.BLLMgzs.Actius()
        With ToolStripComboBoxMgz.ComboBox
            .BindingContext = Me.BindingContext
            .DisplayMember = "Nom"
            .DataSource = oMgzs
            AddHandler .SelectedIndexChanged, AddressOf onMgzChanged
        End With

        Dim oDefaultMgz As DTOMgz = BLL.BLLApp.Mgz
        ToolStripComboBoxMgz.ComboBox.SelectedItem = oMgzs.Find(Function(x) x.Equals(oDefaultMgz))
        'For Each item As DTOMgz In ToolStripComboBoxMgz.ComboBox.Items
        ' If item.Equals(oDefaultMgz) Then
        ' ToolStripComboBoxMgz.ComboBox.SelectedItem = item
        ' Exit For
        ' End If
        'Next
    End Sub

    Private Sub onMgzChanged(sender As Object, e As System.EventArgs)
        If _AllowEvents Then
            LoadArts()
        End If
    End Sub

    Private Sub Xl_ProductBrands1_OnItemSelected(sender As Object, e As MatEventArgs) Handles Xl_ProductBrands1.OnItemSelected
        Dim oBrand As DTOProductBrand = e.Argument
        Dim oTpa As New Tpa(oBrand.Guid)
        Dim oProduct As New Product(oTpa)
        Dim oArgs As New MatEventArgs(oProduct)
        RaiseEvent AfterSelect(Me, oArgs)
        Me.Close()
    End Sub


    Private Sub Xl_ProductCategories1_OnItemSelected(sender As Object, e As MatEventArgs) Handles Xl_ProductCategories1.OnItemSelected
        Select Case _SelMode
            Case SelModes.SelectProduct, SelModes.SelectStp
                Dim oCategory As DTOProductCategory = e.Argument
                Dim oStp As New Stp(oCategory.Guid)
                oStp.Nom = oCategory.Nom
                RaiseEvent AfterSelect(Me, e)
                Me.Close()
        End Select

    End Sub

    Private Sub Xl_StpArts1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_StpArts1.ValueChanged
        Select Case _SelMode
            Case SelModes.SelectProduct, SelModes.SelectArt
                Dim oArt As Art = e.Argument
                Dim oProduct As New Product(oArt)
                Dim oArgs As New MatEventArgs(oProduct)
                RaiseEvent AfterSelect(Me, oArgs)
                Me.Close()
        End Select
    End Sub


    Private Sub RefrescaProductBrands()
        Dim oProductBrands As List(Of DTOProductBrand) = BLL.BLLProductBrands.All(BLL.BLLApp.Emp, True)
        Select Case _SelMode
            Case SelModes.SelectProduct, SelModes.SelectTpa
                Xl_ProductBrands1.Load(oProductBrands, BLL.Defaults.SelectionModes.Selection)
            Case Else
                Xl_ProductBrands1.Load(oProductBrands, BLL.Defaults.SelectionModes.Browse)
        End Select
    End Sub


End Class
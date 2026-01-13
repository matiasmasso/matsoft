Public Class Frm_Inventory
    Private _Mgz As DTOMgz
    Private _values As List(Of DTOProductSku)
    Private _AllowEvents As Boolean


    Public Sub New(Optional oMgz As DTOMgz = Nothing)
        MyBase.New
        InitializeComponent()

        If oMgz Is Nothing Then oMgz = GlobalVariables.Emp.Mgz
        _Mgz = oMgz
    End Sub

    Private Async Sub Frm_Inventory_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Text = "STOCKS " & _Mgz.Nom
        Xl_MatDateTimePicker1.Value = TimeHelper.LastDayOfMonth(DTO.GlobalVariables.Today().AddMonths(-1))
        TextBoxDias50.Text = 90
        TextBoxDias100.Text = 180
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Me.Cursor = Cursors.WaitCursor
        ProgressBar1.Visible = True
        If Await LoadMgzs(CurrentFch, exs) Then
            _values = Await FEB.Mgz.Inventory(exs, _Mgz, Current.Session.User, CurrentFch)
            Me.Cursor = Cursors.Default
            ProgressBar1.Visible = False
            If exs.Count = 0 Then
                LoadBrands()
                LoadCategories()
                LoadSkus()
                _AllowEvents = True
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Function DiesDesvaloritzacio50perCent() As Integer
        Dim RetVal As Integer = 0
        Dim tmp As String = TextBoxDias50.Text
        If IsNumeric(tmp) Then RetVal = CInt(tmp)
        Return RetVal
    End Function

    Private Function DiesDesvaloritzacio100perCent() As Integer
        Dim RetVal As Integer = 0
        Dim tmp As String = TextBoxDias100.Text
        If IsNumeric(tmp) Then RetVal = CInt(tmp)
        Return RetVal
    End Function

    Private Function CurrentFch() As Date
        Return Xl_MatDateTimePicker1.Value
    End Function

    Private Async Function LoadMgzs(DtFch As Date, exs As List(Of Exception)) As Task(Of Boolean)
        Dim oMgzs = Await FEB.Mgzs.Actius(Current.Session.Emp, DtFch, exs)
        If exs.Count = 0 Then
            With ComboBoxMgz
                .DataSource = oMgzs
                .SelectedItem = oMgzs.Find(Function(x) x.Equals(_Mgz))
                .DisplayMember = "Nom"
            End With
        End If
        Return exs.Count = 0
    End Function

    Private Sub LoadBrands()
        Xl_InventoryBrands.Load(_values, DTOProduct.SourceCods.Brand, CurrentFch, TextBoxDias50.Text, TextBoxDias100.Text)
    End Sub

    Private Sub LoadCategories()
        If Xl_InventoryBrands.Value IsNot Nothing Then
            Dim filteredValues = _values.Where(Function(x) x.Category.Brand.Equals(Xl_InventoryBrands.Value)).ToList
            Xl_InventoryCategories.Load(filteredValues, DTOProduct.SourceCods.Category, CurrentFch, TextBoxDias50.Text, TextBoxDias100.Text)
        End If
    End Sub

    Private Sub LoadSkus()
        If Xl_InventoryCategories.Value IsNot Nothing Then
            Dim filteredValues = _values.Where(Function(x) x.Category.Equals(Xl_InventoryCategories.Value)).ToList
            Xl_InventorySkus.Load(filteredValues, DTOProduct.SourceCods.Sku, CurrentFch, TextBoxDias50.Text, TextBoxDias100.Text)
        End If
    End Sub

    Private Sub Xl_InventoryBrands_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_InventoryBrands.ValueChanged
        LoadCategories()
        LoadSkus()
    End Sub

    Private Sub Xl_InventoryCategories_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_InventoryCategories.ValueChanged
        LoadSkus()
    End Sub

    Private Async Sub ButtonRefresca_Click(sender As Object, e As EventArgs) Handles ButtonRefresca.Click
        Await refresca()
    End Sub

    Private Async Sub RECUENTOToolStripButton_Click(sender As Object, e As EventArgs) Handles RECUENTOToolStripButton.Click
        Dim exs As New List(Of Exception)
        Dim oSheet = Await FEB.Mgz.ExcelInventari(exs, _Mgz, Current.Session.User, Xl_MatDateTimePicker1.Value)
        If exs.Count = 0 Then
            If Not UIHelper.ShowExcel(oSheet, exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class
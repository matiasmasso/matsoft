

Public Class Frm_Vehicle_MarcasiModels
    Private mDefaultModel As VehicleModel = Nothing
    Private mAllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Id
        Nom
        Logo
    End Enum

    Public Sub New(ByVal oModel As VehicleModel)
        MyBase.new()
        Me.InitializeComponent()
        mDefaultModel = oModel
        LoadMarcas()
        SetContextMenuMarcas()
        mAllowEvents = True
    End Sub

    Public Sub LoadMarcas()
        Dim SQL As String = "SELECT ID, NOM, LOGO FROM VEHICLE_MARCAS ORDER BY NOM"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)
        mAllowEvents = False
        With DataGridView1
            With .RowTemplate
                .Height = 48 'DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            With .Columns(Cols.Id)
                .Visible = False
            End With
            With .Columns(Cols.Nom)
                .Visible = False
            End With
            With .Columns(Cols.Logo)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
        End With
        mAllowEvents = True
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenuMarcas()
            LoadModels()
        End If
    End Sub

    Private Sub DataGridView2_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView2.SelectionChanged
        If mAllowEvents Then
            SetContextMenuModels()
        End If
    End Sub

    Private Function CurrentMarca() As VehicleMarca
        Dim oMarca As VehicleMarca = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            oMarca = New VehicleMarca(oRow.Cells(Cols.Id).Value)
        End If
        Return oMarca
    End Function

    Private Function CurrentModel() As VehicleModel
        Dim oModel As VehicleModel = Nothing
        Dim oRow As DataGridViewRow = DataGridView2.CurrentRow
        If oRow IsNot Nothing Then
            oModel = New VehicleModel(oRow.Cells(Cols.Id).Value)
        End If
        Return oModel
    End Function

    Private Sub LoadModels()
        Dim SQL As String = "SELECT ID, NOM FROM VEHICLE_MODELS WHERE MARCA=@MARCA ORDER BY NOM"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@MARCA", CurrentMarca.Id)
        Dim oTb As DataTable = oDs.Tables(0)
        With DataGridView2
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            With .Columns(Cols.Id)
                .Visible = False
            End With
            With .Columns(Cols.Nom)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
        SetContextMenuModels()
    End Sub

    Private Sub AddNewMarca(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oMarca As New VehicleMarca
        Dim oFrm As New Frm_VehicleMarca(oMarca)
        AddHandler oFrm.AfterUpdate, AddressOf requestrefreshMarcas
        oFrm.Show()
    End Sub

    Private Sub ZoomMarca(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oMarca As VehicleMarca = CurrentMarca()
        Dim oFrm As New Frm_VehicleMarca(oMarca)
        AddHandler oFrm.AfterUpdate, AddressOf requestrefreshMarcas
        oFrm.Show()
    End Sub

    Private Sub AddNewModel(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oModel As New VehicleModel
        oModel.Marca = CurrentMarca()
        Dim oFrm As New Frm_VehicleModel(oModel)
        AddHandler oFrm.AfterUpdate, AddressOf requestrefreshModels
        oFrm.Show()
    End Sub

    Private Sub ZoomModel(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oModel As VehicleModel = CurrentModel()
        Dim oFrm As New Frm_VehicleModel(oModel)
        AddHandler oFrm.AfterUpdate, AddressOf requestrefreshModels
        oFrm.Show()
    End Sub

    Private Sub SetContextMenuMarcas()
        Dim oContextMenuStrip As New ContextMenuStrip
        Dim oItm As VehicleMarca = CurrentMarca()
        If oItm IsNot Nothing Then
            oContextMenuStrip.Items.Add(New ToolStripMenuItem("zoom", My.Resources.binoculares, AddressOf ZoomMarca))
            oContextMenuStrip.Items.Add(New ToolStripMenuItem("afegir model", Nothing, AddressOf AddNewModel))
        End If
        oContextMenuStrip.Items.Add(New ToolStripMenuItem("nova marca", My.Resources.clip, AddressOf AddNewMarca))
        DataGridView1.ContextMenuStrip = oContextMenuStrip
    End Sub

    Private Sub SetContextMenuModels()
        Dim oContextMenuStrip As New ContextMenuStrip
        Dim oItm As VehicleModel = CurrentModel()
        If oItm IsNot Nothing Then
            oContextMenuStrip.Items.Add(New ToolStripMenuItem("zoom", My.Resources.binoculares, AddressOf ZoomModel))
        End If
        If CurrentMarca() IsNot Nothing Then
            oContextMenuStrip.Items.Add(New ToolStripMenuItem("nou model", My.Resources.clip, AddressOf AddNewModel))
        End If
        DataGridView2.ContextMenuStrip = oContextMenuStrip
    End Sub

    Private Sub requestrefreshModels(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Nom
        Dim oGrid As DataGridView = DataGridView2

        If oGrid.Rows.Count > 0 Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadModels()

        If oGrid.Rows.Count = 0 Then
        Else
            oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = oGrid.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub

    Private Sub requestrefreshMarcas(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim oGrid As DataGridView = DataGridView1
        Dim iFirstVisibleCell As Integer = oGrid.Columns.GetFirstColumn(DataGridViewElementStates.Visible).Index

        If oGrid.Rows.Count > 0 Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadMarcas()
        Dim oModel As VehicleModel = CurrentModel()
        If oModel IsNot Nothing Then
            If oModel.Marca IsNot CurrentMarca() Then
                LoadModels()
            End If
        End If

        If oGrid.Rows.Count = 0 Then
        Else
            oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = oGrid.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub


    Private Sub DataGridView2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView2.DoubleClick
        Dim oModel As VehicleModel = CurrentModel()
        If oModel IsNot Nothing Then
            RaiseEvent AfterUpdate(CurrentModel, EventArgs.Empty)
            Me.Close()
        End If
    End Sub
End Class
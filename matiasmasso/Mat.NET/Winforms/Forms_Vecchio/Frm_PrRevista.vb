
Imports System.Data.SqlClient

Public Class Frm_PrRevista
    Private mRevista As PrRevista
    Private mMode As Modes = Modes.Consulta
    Private mallowevents As Boolean = False

    Public Event AfterSelect(ByVal sender As Object, ByVal e As EventArgs)
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As EventArgs)

    Private Enum Tabs
        General
        Revistes
        Tarifas
        Ordres
        Insercions
    End Enum

    Private Enum ColsNumeros
        Guid
        Text
        Thumb
    End Enum

    Private Enum ColsTarifas
        SizeCod
        SizeNom
        Tarifa
        Dto
        Net1
        Dt2
        Net2
    End Enum

    Public Enum Modes
        Consulta
        SelectNumero
    End Enum

    Public Sub New(ByVal oMode As Modes, ByVal oRevista As PrRevista, Optional ByVal oNumero As PrNumero = Nothing)
        MyBase.New()
        Me.InitializeComponent()
        mMode = oMode
        mRevista = oRevista
        With mRevista
            TextBoxNom.Text = .Nom
            NumericUpDownPageWidth.Value = .PageSize.Width
            NumericUpDownPageHeight.Value = .PageSize.Height
            NumericUpDownSang.Value = .Sang
            Xl_ImageLogo.Bitmap = .Logo
        End With

        If oNumero IsNot Nothing Then
            TabControl1.SelectedIndex = Tabs.Revistes
            Dim oPrNumeros As PrNumeros = PrNumeroLoader.Find(mRevista)
            Xl_PrNumeros1.Load(oPrNumeros)
        End If
        mallowevents = True
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.General
            Case Tabs.Revistes
                Static BlLoadedNumeros As Boolean
                If Not BlLoadedNumeros Then
                    Dim oPrNumeros As PrNumeros = PrNumeroLoader.Find(mRevista)
                    Xl_PrNumeros1.Load(oPrNumeros)
                    BlLoadedNumeros = True
                End If
            Case Tabs.Tarifas
                Static BlLoadedTarifas As Boolean
                If Not BlLoadedTarifas Then
                    LoadYeaTarifas()
                    LoadGridTarifas()
                    SetContextMenuTarifas()
                    BlLoadedTarifas = True
                End If
            Case Tabs.Ordres
                Static BlLoadedOrdres As Boolean
                If Not BlLoadedOrdres Then
                    Xl_PrOrdresDeCompra1.Revista = mRevista
                    BlLoadedOrdres = True
                End If
            Case Tabs.Insercions
                Static BlLoadedInsercions As Boolean
                If Not BlLoadedInsercions Then
                    LoadInsercions
                    BlLoadedInsercions = True
                End If
        End Select
    End Sub

    Private Sub LoadInsercions()
        Dim oPrInsercions As PrInsercions = PrInsercioLoader.FromRevista(mRevista)
        Xl_PrInsercions1.Load(oPrInsercions)
    End Sub

    Private Sub Xl_PrInsercions1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_PrInsercions1.RequestToRefresh
        LoadInsercions()
    End Sub

    Private Sub LoadYeaTarifas()
        Dim SQL As String = "SELECT MIN(YEA) AS MINYEA, MAX(YEA) AS MAXYEA FROM PRTARIFAS WHERE REVISTA LIKE @REVISTA"
        Dim oDrd As SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi, "@REVISTA", mRevista.Guid.ToString)
        oDrd.Read()
        If IsDBNull(oDrd("MINYEA")) Then
            With NumericUpDownYeaTarifas
                .Maximum = Today.Year
                .Minimum = Today.Year
                .Value = Today.Year
            End With
        Else
            With NumericUpDownYeaTarifas
                .Maximum = CInt(oDrd("MAXYEA"))
                .Value = CInt(oDrd("MAXYEA"))
            End With
        End If
    End Sub

    Private Sub LoadGridTarifas()
        Dim SQL As String = "SELECT SIZECOD, '' AS SIZE, EUR,DTO," _
        & "ROUND(EUR*(100-DTO)/100,2) AS NET1,DT2," _
        & "ROUND(ROUND(EUR*(100-DTO)/100,2)*(100-DT2)/100,2) AS NET2 " _
        & "FROM PRTARIFAS WHERE REVISTA LIKE @REVISTA AND YEA=@YEA " _
        & "ORDER BY SIZECOD"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@REVISTA", mRevista.Guid.ToString, "@YEA", CurrentYeaTarifa)
        Dim oTb As DataTable = oDs.Tables(0)

        mallowevents = False
        With DataGridViewTarifas
            .RowTemplate.Height = .Font.Height * 1.3
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .DefaultCellStyle.SelectionBackColor = Color.AliceBlue
            .DefaultCellStyle.SelectionForeColor = Color.Black
            .DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToAddRows = False
            .AllowUserToResizeRows = False

            With .Columns(ColsTarifas.SizeCod)
                .Visible = False
            End With
            With .Columns(ColsTarifas.SizeNom)
                .HeaderText = "concepte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
            With .Columns(ColsTarifas.Tarifa)
                .HeaderText = "tarifa"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsTarifas.Dto)
                .HeaderText = "dte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 30
                .DefaultCellStyle.Format = "#\%;-#\%;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsTarifas.Net1)
                .HeaderText = "neto"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsTarifas.Dt2)
                .HeaderText = "dte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 30
                .DefaultCellStyle.Format = "#\%;-#\%;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsTarifas.Net2)
                .HeaderText = "a pagar"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With
        mallowevents = True
    End Sub

    Private Function CurrentYeaTarifa() As Integer
        Dim RetVal As Integer = NumericUpDownYeaTarifas.Value
        Return RetVal
    End Function

    Private Function CurrentTarifa() As PrTarifa
        Dim oRetVal As PrTarifa = Nothing
        'Dim oRow As DataGridViewRow = DataGridViewNumeros.CurrentRow
        'If oRow IsNot Nothing Then
        ' Dim oSizeCod As PrInsercio.SizeCods
        ' oRetVal = New PrTarifa(CurrentYeaTarifa, mRevista, oSizeCod)
        ' End If
        Return oRetVal
    End Function

    Private Sub NumericUpDownYeaTarifas_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles NumericUpDownYeaTarifas.ValueChanged
        If mallowevents Then
            LoadGridTarifas()
            SetContextMenuTarifas()
        End If
    End Sub

    Private Sub SetContextMenuTarifas()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        oMenuItem = New ToolStripMenuItem("Zoom", Nothing, AddressOf ZoomTarifa)
        oMenuItem.Enabled = (CurrentTarifa() IsNot Nothing)
        oContextMenu.Items.Add(oMenuItem)

        Dim s As String = ""
        Dim v As Integer
        For Each v In [Enum].GetValues(GetType(PrInsercio.SizeCods))
            If CType(v, PrInsercio.SizeCods) <> PrInsercio.SizeCods.NotSet Then
                s = PrInsercio.GetSizeCodName(v)
                oMenuItem = New ToolStripMenuItem("Afegir preu " & s, Nothing, AddressOf AddNewTarifa)
                oContextMenu.Items.Add(oMenuItem)
            End If
        Next

        DataGridViewTarifas.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub ZoomTarifa(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oTarifa As PrTarifa = CurrentTarifa()
        Dim oFrm As New Frm_PrTarifa(oTarifa)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestTarifas
        oFrm.Show()
    End Sub

    Private Sub AddNewTarifa(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oMenuItem As ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)
        Dim oSizeCod As PrInsercio.SizeCods = GetSizeCodFromMenuItem(sender)
        Dim oTarifa As New PrTarifa(CurrentYeaTarifa, mRevista, oSizeCod)
        Dim oFrm As New Frm_PrTarifa(oTarifa)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestTarifas
        oFrm.Show()
    End Sub


    Private Function GetSizeCodFromMenuItem(ByVal oMenuItem As ToolStripMenuItem) As PrInsercio.SizeCods
        Dim sMenuName As String = oMenuItem.Text

        Dim s As String = ""
        Dim v As Integer
        For Each v In [Enum].GetValues(GetType(PrInsercio.SizeCods))
            If CType(v, PrInsercio.SizeCods) <> PrInsercio.SizeCods.NotSet Then
                s = PrInsercio.GetSizeCodName(v)
                If sMenuName.Contains(s) Then
                    Exit For
                End If
            End If
        Next

        Return v
    End Function

    Private Sub RefreshRequestTarifas(ByVal sender As Object, ByVal e As System.EventArgs)
        If DirectCast(sender, PrTarifa).Yea <> CurrentYeaTarifa() Then
            LoadYeaTarifas()
        End If

        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = ColsTarifas.SizeNom
        Dim oGrid As DataGridView = DataGridViewTarifas
        Dim oRow As DataGridViewRow = oGrid.CurrentRow

        If oRow IsNot Nothing Then
            i = oRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadGridTarifas()

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

    Private Sub RefreshRequestNumeros(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oPrNumeros As PrNumeros = PrNumeroLoader.Find(mRevista)
        Xl_PrNumeros1.Load(oPrNumeros)
    End Sub

    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    Xl_ImageLogo.AfterUpdate, _
     TextBoxNom.TextChanged, _
      NumericUpDownPageWidth.ValueChanged, _
       NumericUpDownPageHeight.ValueChanged, _
        NumericUpDownSang.ValueChanged

        If mallowevents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mRevista
            .Logo = Xl_ImageLogo.Bitmap
            .Nom = TextBoxNom.Text
            .PageSize = New Size(NumericUpDownPageWidth.Value, NumericUpDownPageHeight.Value)
            .Sang = NumericUpDownSang.Value
            .Update()
        End With
        RaiseEvent AfterUpdate(mRevista, EventArgs.Empty)
        Me.Close()
    End Sub

    Private Sub DataGridViewTarifas_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridViewTarifas.CellFormatting
        Select Case e.ColumnIndex
            Case ColsTarifas.SizeNom
                Dim oRow As DataGridViewRow = DataGridViewTarifas.Rows(e.RowIndex)
                Dim oCod As PrInsercio.SizeCods = oRow.Cells(ColsTarifas.SizeCod).Value
                Dim sNom As String = maxisrvr.GetStringNomFromEnumerationValue(GetType(PrInsercio.SizeCods), oCod)
                e.Value = sNom
        End Select
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub Xl_PrNumeros1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_PrNumeros1.RequestToAddNew
        Dim oNumero As New PrNumero(mRevista)
        Dim oFrm As New Frm_PrNumero(oNumero)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestNumeros
        oFrm.Show()
    End Sub
End Class
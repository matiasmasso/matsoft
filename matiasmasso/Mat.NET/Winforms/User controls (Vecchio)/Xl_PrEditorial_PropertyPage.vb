Public Class Xl_PrEditorial_PropertyPage
    Private mEditorial As PrEditorial

    Private Enum Tabs
        General
        Revistes
        Ordres
        Insercions
    End Enum

    Private Enum ColsRevistes
        Guid
        Logo
    End Enum


    Public Sub New(ByVal oEditorial As PrEditorial)
        MyBase.New()
        Me.InitializeComponent()
        mEditorial = oEditorial
        TextBoxNom.Text = mEditorial.Nom
        Xl_Contact_Logo1.Contact = mEditorial
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.General
            Case Tabs.Revistes
                Static BlLoadedRevistes As Boolean
                If Not BlLoadedRevistes Then
                    LoadGridRevistes()
                    BlLoadedRevistes = True
                    SetContextMenuRevistes()
                End If
            Case Tabs.Ordres
                Static BlLoadedOrdres As Boolean
                If Not BlLoadedOrdres Then Xl_PrOrdresDeCompra1.Editorial = mEditorial
                BlLoadedOrdres = True
            Case Tabs.Insercions
                Static BlLoadedInsercions As Boolean
                If Not BlLoadedInsercions Then
                    LoadInsercions()
                    BlLoadedInsercions = True
                End If
                BlLoadedInsercions = True
        End Select
    End Sub

    Private Sub LoadInsercions()
        Dim oPrInsercions As PrInsercions = PrInsercioLoader.FromEditorial(mEditorial)
        Xl_PrInsercions1.Load(oPrInsercions)
    End Sub

    Private Sub Xl_PrInsercions1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_PrInsercions1.RequestToRefresh
        LoadInsercions()
    End Sub

    Private Sub LoadGridRevistes()
        Dim SQL As String = "SELECT GUID,LOGO FROM PRREVISTAS " _
        & "WHERE EMP=@EMP AND EDITORIAL=@EDITORIAL " _
        & "ORDER BY NOM"
        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi, "@EMP", mEditorial.Emp.Id, "@EDITORIAL", mEditorial.Id)
        Dim oTb As DataTable = oDs.Tables(0)


        With DataGridViewRevistes
            With .RowTemplate
                .Height = 48
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToAddRows = False
            .AllowUserToResizeRows = False

            With .Columns(ColsRevistes.Guid)
                .Visible = False
            End With
            With .Columns(ColsRevistes.Logo)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
    End Sub

    Private Function CurrentRevista() As PrRevista
        Dim oRevista As PrRevista = Nothing
        Dim oRow As DataGridViewRow = DataGridViewRevistes.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As New System.Guid(oRow.Cells(ColsRevistes.Guid).Value.ToString)
            oRevista = New PrRevista(oGuid)
        End If
        Return oRevista
    End Function

    Private Function CurrentRevistas() As PrRevistas
        Dim oRevistas As New PrRevistas
        Dim oGuid As System.Guid = System.Guid.Empty

        If DataGridViewRevistes.SelectedRows.Count > 0 Then
            Dim oRevista As PrRevista
            Dim oRow As DataGridViewRow
            For Each oRow In DataGridViewRevistes.SelectedRows
                oGuid = New System.Guid(oRow.Cells(ColsRevistes.Guid).Value.ToString)
                oRevista = New PrRevista(oGuid)
                oRevistas.Add(oRevista)
            Next
        Else
            Dim oRevista As PrRevista = CurrentRevista()
            If oRevista IsNot Nothing Then
                oRevistas.Add(CurrentRevista)
            End If
        End If
        Return oRevistas
    End Function

    Private Sub DataGridViewRevistes_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewRevistes.DoubleClick
        'Dim oFrm As New Frm_PrRevista(CurrentRevista)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestRevistes
        'With oFrm
        '.Revista = CurrentRevista()
        '.Show()
        'End With
    End Sub

    Private Sub DataGridViewRevistes_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewRevistes.SelectionChanged
        SetContextMenuRevistes()
    End Sub

    Private Sub SetContextMenuRevistes()
        Dim oContextMenu As New ContextMenuStrip
        Dim oRevistas As PrRevistas = CurrentRevistas()

        If oRevistas.Count > 0 Then
            'Dim oMenu_Revista As New Menu_PrRevista(oRevistas)
            'AddHandler oMenu_Revista.AfterUpdate, AddressOf RefreshRequestRevistes
            'oContextMenu.Items.AddRange(oMenu_Revista.Range)
        End If

        DataGridViewRevistes.ContextMenuStrip = oContextMenu
    End Sub


    Private Sub RefreshRequestRevistes(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = ColsRevistes.Logo
        Dim oGrid As DataGridView = DataGridViewRevistes

        If oGrid.Rows.Count > 0 Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadGridRevistes()

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


    Private Sub DataGridViewRevistes_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridViewRevistes.CellFormatting
        Select Case e.ColumnIndex
            Case ColsRevistes.Logo
                If IsDBNull(e.Value) Then
                    e.Value = My.Resources.empty
                End If
        End Select
    End Sub

End Class
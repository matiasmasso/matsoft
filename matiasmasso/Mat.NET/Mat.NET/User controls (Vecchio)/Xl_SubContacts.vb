Public Class Xl_SubContacts
    Public Event Changed()

    Private mSubContacts As MaxiSrvr.SubContacts
    Private mTb As DataTable

    Public Property Subcontacts() As MaxiSrvr.SubContacts
        Get
            Return ReadFromGrid()
        End Get
        Set(ByVal Value As MaxiSrvr.SubContacts)
            If Value IsNot Nothing Then
                WriteToGrid(Value)
            End If
        End Set
    End Property

    Private Sub WriteToGrid(ByVal oScs As MaxiSrvr.SubContacts)
        mTb = CreateDatatable(oScs)
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = mTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .BackgroundColor = Color.LightYellow
            With .Columns(0)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
    End Sub

    Private Function ReadFromGrid() As MaxiSrvr.SubContacts
        Dim oRetVal As New MaxiSrvr.SubContacts
        Dim oRow As DataRow
        Dim oSc As String
        For Each oRow In mTb.Rows
            If oRow(0) <> Nothing Then
                oSc = oRow(0)
                If oSc > "" Then oRetVal.Add(oSc)
            End If
        Next
        Return oRetVal
    End Function

    Private Function CreateDatatable(ByVal oScs As MaxiSrvr.SubContacts) As DataTable
        Dim oTb As New DataTable("SUBCONTACTS")
        oTb.Columns.Add("TEXT", System.Type.GetType("System.String"))
        Dim oSc As String
        Dim oRow As DataRow
        For Each oSc In oScs
            oRow = oTb.NewRow()
            oRow(0) = oSc
            oTb.Rows.Add(oRow)
        Next
        Return oTb
    End Function


    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        RaiseEvent Changed()
    End Sub

    Private Sub DataGridView1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles DataGridView1.RowsAdded
        RaiseEvent Changed()
    End Sub

    Private Sub DataGridView1_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles DataGridView1.RowsRemoved
        RaiseEvent Changed()
    End Sub
End Class

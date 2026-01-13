
Public Class Frm_Ports
    Private mallowevents As Boolean
    Private mLastPort As maxisrvr.MatPort = Nothing
    Private mDirty As Boolean = False

    Private Enum Cols
        Id
        Nom
    End Enum

    Private Sub Frm_Ports_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadPorts()
        mallowevents = True
    End Sub

    Private Sub LoadPorts()
        Dim SQL As String = "SELECT ID,NOM FROM PORTS ORDER BY NOM,ID"
        Dim oDs As DataSet =  DAL.SQLHelper.GetDataset(SQL, New List(Of Exception))
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow = Nothing
        For Each oRow In oTb.Rows
            If oRow("NOM") = "" Then
                oRow("NOM") = "(" & CType(oRow("ID"), maxisrvr.MatPort.Ids).ToString & ")"
            End If
        Next

        For Each v As maxisrvr.MatPort.Ids In [Enum].GetValues(GetType(maxisrvr.MatPort.Ids))
            If v <> maxisrvr.MatPort.Ids.NotSet Then
                Dim BlMissingValue As Boolean = True
                For Each oRow In oTb.Rows
                    Dim iRowId As Integer = CInt(oRow("ID"))
                    Dim iId As Integer = CInt(v)
                    If iRowId = iId Then
                        BlMissingValue = False
                        Exit For
                    End If
                Next
                If BlMissingValue Then
                    oRow = oTb.NewRow
                    oRow("ID") = CInt(v)
                    oRow("NOM") = "(" & v.ToString & ")"
                    oTb.Rows.Add(oRow)
                End If
            End If
        Next

        With DataGridView1
            .DataSource = oTb
            .RowHeadersVisible = False
            .ColumnHeadersVisible = False
            With .Columns(Cols.Id)
                .Visible = False
            End With
            With .Columns(Cols.Nom)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With

        If oTb.Rows.Count > 0 Then
            mLastPort = CurrentPort()
            PropertyGrid1.SelectedObject = mLastPort
        End If
    End Sub

    Private Function CurrentPort() As maxisrvr.MatPort
        Dim oPort As maxisrvr.MatPort = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            oPort = New maxisrvr.MatPort(oRow.Cells(0).Value)

        End If
        Return oPort
    End Function



    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mallowevents Then
            If mDirty And mLastPort IsNot Nothing Then
                Dim rc As MsgBoxResult = MsgBox("Guardem els canvis?", MsgBoxStyle.YesNo, "PORT " & mLastPort.Nom)
                Select Case rc
                    Case MsgBoxResult.Yes
                        Dim oPort As maxisrvr.MatPort = PropertyGrid1.SelectedObject
                        oPort.Update()
                    Case MsgBoxResult.No
                End Select
                mDirty = False
            End If
            mLastPort = CurrentPort()
            PropertyGrid1.SelectedObject = mLastPort
        End If
    End Sub

    Private Sub PropertyGrid1_PropertyValueChanged(ByVal s As Object, ByVal e As System.Windows.Forms.PropertyValueChangedEventArgs) Handles PropertyGrid1.PropertyValueChanged
        mDirty = True
    End Sub


    Private Sub Frm_Ports_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If mDirty Then
            Dim rc As MsgBoxResult = MsgBox("Guardem els canvis?", MsgBoxStyle.YesNoCancel, "PORT " & mLastPort.Nom)
            Select Case rc
                Case MsgBoxResult.Yes
                    Dim oPort As maxisrvr.MatPort = PropertyGrid1.SelectedObject
                    oPort.Update()
                Case MsgBoxResult.No
                Case MsgBoxResult.Cancel
                    e.Cancel = True
            End Select
        End If
    End Sub


End Class
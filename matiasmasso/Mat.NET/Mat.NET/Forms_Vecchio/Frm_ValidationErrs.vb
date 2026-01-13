Public Class Frm_ValidationErrs
    Private mErrs as List(Of exception)

    Public Event GoAhead(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal sTitle As String, ByVal exs as List(Of exception))
        MyBase.New()
        Me.InitializeComponent()
        Me.Text = sTitle
        mErrs = exs
        LoadGrid()
    End Sub

    Private Sub LoadGrid()
        With DataGridView1
            .Columns.Add(New DataGridViewTextBoxColumn)
            For Each oErr As Exception In mErrs
                .Rows.Add(oErr.Message)
            Next
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowUserToResizeColumns = True
            With .Columns(0)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
    End Sub


    Private Sub ButtonGoAhead_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonGoAhead.Click
        RaiseEvent GoAhead(mErrs, EventArgs.Empty)
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub
End Class
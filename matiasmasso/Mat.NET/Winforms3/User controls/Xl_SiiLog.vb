Public Class Xl_SiiLog
    Private _value As DTOSiiLog

    Public Shadows Sub Load(value As DTOSiiLog)
        _value = value
        If _value Is Nothing OrElse value.Result = DTOSiiLog.Results.NotSet Then
            TextBoxFch.Text = "(pendent)"
        Else
            PictureBox1.Image = IconHelper.SiiResult(_value)
            TextBoxFch.Text = Format(value.Fch, "dd/MM/yy HH:mm")
            ToolTip1.SetToolTip(PictureBox1, DTOSiiLog.ResultText(_value))
            TextBoxCsv.Text = value.Csv & " " & value.ErrMsg
        End If
    End Sub

    Private Sub Xl_SiiLog_DoubleClick(sender As Object, e As EventArgs) Handles _
        Me.DoubleClick,
         TextBoxFch.DoubleClick,
          PictureBox1.DoubleClick

        Do_Zoom()
    End Sub

    Private Sub SetContextMenuStrip()
        Dim oContextMenu As New ContextMenuStrip
        oContextMenu.Items.Add("zoom", Nothing, AddressOf Do_Zoom)
        MyBase.ContextMenuStrip = oContextMenu
        PictureBox1.ContextMenuStrip = oContextMenu
        TextBoxFch.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_Zoom()
        'Dim oFrm As New Frm_SiiLog(_value)
        'oFrm.Show()
    End Sub

End Class

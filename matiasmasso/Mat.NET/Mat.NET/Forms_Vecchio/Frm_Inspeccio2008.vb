

Public Class Frm_Inspeccio2008

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If IsNumeric(TextBox1.Text) Then
            Dim iYea As Integer = CInt(NumericUpDown1.Value)
            Dim iId As Integer = CInt(TextBox1.Text)
            Dim oCca As Cca = Cca.FromRenumeratedIndex(BLL.BLLApp.Emp, iYea, iId)
            If oCca Is Nothing Then
                MsgBox("assentament inexistent", vbExclamation, "MAT.NET")
            Else
                Dim oFrm As New Frm_Cca(oCca)
                oFrm.Show()
            End If
        Else
            MsgBox("numero incorrecte", vbExclamation, "MAT.NET")
        End If
    End Sub
End Class
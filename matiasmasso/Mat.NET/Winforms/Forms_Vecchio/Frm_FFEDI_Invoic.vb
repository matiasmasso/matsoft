

Public Class Frm_FFEDI_Invoic

    Private Sub ImportarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImportarToolStripMenuItem.Click
        Dim oDlg As New OpenFileDialog
        With oDlg
            If .ShowDialog Then
                Dim sFileName As String = .FileName
                Dim oId As DTO.DTOFlatFile.Ids = FF.GetFileId(sFileName)
                Select Case oId
                    Case DTO.DTOFlatFile.Ids.eDiversa_OrdersD96A
                        'PropertyGrid1.SelectedObject = New FF_EDIVERSA_ORDERS(sFileName)
                    Case DTO.DTOFlatFile.Ids.eDiversa_DesadvD96A
                        'PropertyGrid1.SelectedObject = Alb.FromDesadv(New FF_EDIVERSA_DESADV(sFileName))
                    Case DTO.DTOFlatFile.Ids.eDiversa_InvoiceD96A
                        PropertyGrid1.SelectedObject = New FF_EDIVERSA_INVOIC(sFileName)
                    Case Else
                        MsgBox("fitxer de format desconegut", MsgBoxStyle.Exclamation, "MAT.NET")
                End Select
            End If
        End With
    End Sub
End Class
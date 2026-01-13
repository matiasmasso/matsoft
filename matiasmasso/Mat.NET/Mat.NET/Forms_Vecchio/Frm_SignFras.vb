

Public Class Frm_SignFras
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mDrd As SqlClient.SqlDataReader
    Private mCancel As Boolean

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        RegeneraFactures(TextBoxYea.Text)
    End Sub

    Private Sub RegeneraFactures(ByVal iYea As Integer)
        Dim SQL As String = "SELECT Guid FROM FRA " _
        & "INNER JOIN CCA ON FRA.CCAGUID=CCA.GUID WHERE fra.EMP=1 AND fra.YEA=" & iYea & "  ORDER BY fra.FRA"
        mDrd = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi)
        Dim oFra As Fra = Nothing
        Do While mDrd.Read
            Try
                If mCancel Then
                    Dim rc As MsgBoxResult = MsgBox("detenim el programa?", MsgBoxStyle.OkCancel, "REGENERA PDF FACTURES")
                    If rc = MsgBoxResult.Ok Then
                        mDrd.Close()
                        Me.Close()
                        Exit Sub
                    End If
                End If
                Dim oGuid As Guid = mDrd("Guid")
                oFra = New Fra(oGuid)
                LabelStatus.Text = "fra." & oFra.Id & " del " & oFra.Fch
                Application.DoEvents()

                Dim oCca As Cca = oFra.Cca

                Dim exs as New List(Of exception)
                Dim oPdf As New PdfAlb(oFra)

                Dim oDocFile As DTODocFile = Nothing
                If BLL_DocFile.LoadFromStream(oPdf.Stream(True), oDocFile, exs) Then
                    oCca.DocFile = oDocFile
                    oCca.DocFile.PendingOp = DTODocFile.PendingOps.Update
                Else
                    UIHelper.WarnError(exs, "error al redactar o al signar la factura " & oFra.Id)
                End If

                If Not oCca.Update( exs) Then
                    MsgBox("error" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
                End If

            Catch ex As Exception
                Dim sId As String = ""
                If oFra IsNot Nothing Then sId = oFra.Id
                BLL.MailHelper.MailAdmin("SIGNFRA ERR " & sId & " " & ex.Message)
            End Try

        Loop
        mDrd.Close()
        MsgBox("pdf de factures " & iYea & " regenerats", MsgBoxStyle.Information, "MAT.NET")

    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        mcancel = True
    End Sub
End Class
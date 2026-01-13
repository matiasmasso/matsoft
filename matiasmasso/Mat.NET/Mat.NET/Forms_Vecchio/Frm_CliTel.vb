Imports System.Data.SqlClient


Public Class Frm_CliTel
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mTel As tel

    Public WriteOnly Property Tel() As tel
        Set(ByVal value As tel)
            mTel = value
            TextBoxTel.Text = mTel.Num
            SetCli()
            SetLineaCod()
            CheckBoxPrivat.Checked = mTel.Privat
            TextBoxObs.Text = mTel.Obs
        End Set
    End Property

      Private Sub SetCli()
        Dim SQL As String = "SELECT CAST(ID AS INT) as ID, CLX.CLX AS NOM FROM " _
        & "(SELECT EMP, MIN(CLITEL.CLI) AS ID, CLITEL.NUM FROM CLITEL WHERE COD<4 GROUP BY CLITEL.EMP,CLITEL.NUM) TEL " _
        & "INNER JOIN CLX ON TEL.EMP=CLX.EMP AND TEL.ID=CLX.CLI WHERE CLX.EMP=1 AND TEL.NUM LIKE '" & mTel.Num & "'"
        Dim oDrd As SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi)
        If oDrd.Read Then
            Dim oContact As Contact = MaxiSrvr.Contact.FromNum(mEmp, oDrd("ID"))
        End If
        oDrd.Close()
    End Sub

    Private Sub SetLineaCod()
        Select Case mTel.Cod
            Case MaxiSrvr.Tel.Cods.movil
                RadioButtonMovil.Checked = True
            Case MaxiSrvr.Tel.Cods.fax
                RadioButtonFax.Checked = True
            Case MaxiSrvr.Tel.Cods.tel
                RadioButtonTel.Checked = True
        End Select
    End Sub

    Private Sub RadioButtonTel_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    RadioButtonTel.CheckedChanged, RadioButtonFax.CheckedChanged, RadioButtonMovil.CheckedChanged
        If RadioButtonTel.Checked Then
            PictureBoxIco.Image = My.Resources.tel
        End If
        If RadioButtonFax.Checked Then
            PictureBoxIco.Image = My.Resources.fax
        End If
        If RadioButtonMovil.Checked Then
            PictureBoxIco.Image = My.Resources.movil
        End If
    End Sub

    Private Function GetCod() As Tel.Cods
        If RadioButtonFax.Checked Then
            Return MaxiSrvr.Tel.Cods.fax
        End If
        If RadioButtonMovil.Checked Then
            Return MaxiSrvr.Tel.Cods.movil
        End If
        Return MaxiSrvr.Tel.Cods.tel
    End Function

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim oContact As Contact = Xl_Contact1.Contact
        If oContact.Exists Then
            With mTel
                .Cod = GetCod()
                .Obs = TextBoxObs.Text
                .Privat = CheckBoxPrivat.Checked
                .Country = oContact.Adr.Zip.Location.Zona.Country
            End With
            oContact.Tels.Add(mTel)
            oContact.UpdateTels()
            Me.Close()
        Else
            MsgBox("client inexistent", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub
End Class
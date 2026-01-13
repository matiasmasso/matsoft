

Public Class Frm_Config

    Private mAllowEvents As Boolean
    Private mEmp as DTOEmp = Current.session.emp
    Private mDirtySched As Boolean

    'MGZNEXTTRANSM

    Private Async Sub Frm_Config_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        Dim s As String = Await FEB.Default.EmpValue(Current.Session.Emp, DTODefault.Codis.MinAlbDate, exs)
        TextBoxLASTBLOCKEDCCAYEA.Text = Await FEB.Default.EmpValue(Current.Session.Emp, DTODefault.Codis.LastBlockedCcaYea, exs)
        If exs.Count = 0 Then
            If IsDate(s) Then
                DateTimePickerMinAlbDate.Value = CDate(s)
            End If

            's = mEmp.GetDefault("MGZNEXTTRANSM")
            'If IsDate(s) Then
            'DateTimePickerMgzNextTransm.Value = mEmp.GetDefault("MGZNEXTTRANSM")
            'End If
            'TextBoxMgzEmail.Text = mEmp.GetDefault("VIVACE")
            mAllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub



    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
 _
    TextBoxMgzEmail.TextChanged, _
    DateTimePickerMinAlbDate.ValueChanged
        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        'mEmp.SetDefault("MINALBDATE", DateTimePickerMinAlbDate.Value)
        ' mEmp.SetDefault("MGZNEXTTRANSM", DateTimePickerMgzNextTransm.Value)
        'mEmp.SetDefault("VIVACE", TextBoxMgzEmail.Text)
        'mEmp.SetDefault("LASTBLOCKEDCCAYEA", TextBoxLASTBLOCKEDCCAYEA.Text)

        If mDirtySched Then
            'Dim SQL As String
            'SQL = "UPDATE SCHED SET ACTIVE=" & IIf(CheckBoxLaborables.Checked, 1, 0) & ", " _
            '& "ELAPSEHORA=" & DateTimePickerMatins.Value.Hour & ", " _
            '& "ELAPSEMINUTS=" & DateTimePickerMatins.Value.Minute & " " _
            '& "WHERE ID=30"
            'maxisrvr.executenonquery(SQL, maxisrvr.Databases.Maxi)
            'SQL = "UPDATE SCHED SET ACTIVE=" & IIf(CheckBoxTardes.Checked, 1, 0) & ", " _
            '& "ELAPSEHORA=" & DateTimePickerTardes.Value.Hour & ", " _
            '& "ELAPSEMINUTS=" & DateTimePickerTardes.Value.Minute & " " _
            '& "WHERE ID=31"
            'maxisrvr.executenonquery(SQL, maxisrvr.Databases.Maxi)
        End If
        Me.Close()
    End Sub

    Private Sub Control_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    TextBoxLASTBLOCKEDCCAYEA.TextChanged, _
    DateTimePickerMinAlbDate.ValueChanged, _
    TextBoxMgzEmail.TextChanged
        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub


    Private Sub DateTimePickerSched_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If mAllowEvents Then
            mDirtySched = True
            ButtonOk.Enabled = True
        End If
    End Sub
End Class
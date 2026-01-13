

Public Class Frm_RepCliCom

    Private mRepCliCom As RepCliCom
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oRepCliCom As RepCliCom)
        MyBase.new()
        Me.InitializeComponent()
        mRepCliCom = oRepCliCom
        Me.Text = "excepció de comisions"
        Refresca()
        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        With mRepCliCom
            TextBoxRep.Text = .Rep.Abr
            Xl_Contact1.Contact = .Cli
            If .Exists Then
                Xl_Contact1.Enabled = False
                Xl_Percent1.Value = .Com
                DateTimePickerFchFrom.Value = .FchFrom
                If .FchTo = Nothing Then
                    CheckBoxObsolet.Checked = False
                    DateTimePickerFchTo.Visible = False
                Else
                    CheckBoxObsolet.Checked = True
                    DateTimePickerFchTo.Visible = True
                    DateTimePickerFchTo.Value = .FchTo
                End If
                ButtonDel.Enabled = True
            End If
        End With
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
         Xl_Contact1.AfterUpdate, _
          Xl_Percent1.AfterUpdate, _
           DateTimePickerFchFrom.ValueChanged, _
            DateTimePickerFchTo.ValueChanged

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        If mRepCliCom.Cli Is Nothing Then
            Dim oRep As Rep = mRepCliCom.Rep
            mRepCliCom = New RepCliCom(oRep, New Client(Xl_Contact1.Contact.Guid))
        End If

        With mRepCliCom
            .Com = Xl_Percent1.Value
            .FchFrom = DateTimePickerFchFrom.Value
            If CheckBoxObsolet.Checked Then
                .FchTo = DateTimePickerFchTo.Value
            Else
                .FchTo = Nothing
            End If

            .Update()
            RaiseEvent AfterUpdate(mRepCliCom, System.EventArgs.Empty)
            Me.Close()
        End With
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        If mRepCliCom.AllowDelete Then
            mRepCliCom.Delete()
            Me.Close()
        End If
    End Sub

    Private Sub CheckBoxObsolet_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxObsolet.CheckedChanged
        If mAllowEvents Then
            DateTimePickerFchTo.Visible = CheckBoxObsolet.Checked
            ButtonOk.Enabled = True
        End If
    End Sub
End Class
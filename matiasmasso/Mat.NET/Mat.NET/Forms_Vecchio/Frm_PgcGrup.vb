

Public Class Frm_PgcGrup
    Private mPgcGrup As PgcGrup
    Private mParentGrup As PgcGrup
    Private mAllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public WriteOnly Property PgcGrup() As PgcGrup
        Set(ByVal value As PgcGrup)
            mPgcGrup = value
            Refresca()
        End Set
    End Property

    Public WriteOnly Property ParentGrup() As PgcGrup
        Set(ByVal value As PgcGrup)
            mParentGrup = value
            mPgcGrup = mParentGrup.newchild
            Refresca()
        End Set
    End Property

    Private Sub Refresca()
        With mPgcGrup
            If .Exists Then
                Me.Text = "GRUP DE COMPTES"
                ButtonDel.Enabled = True
                TextBoxId.Text = .Id
            Else
                Me.Text = "NOU GRUP DE COMPTES"
                ButtonDel.Enabled = False
                TextBoxId.Text = .Id
            End If
            SetCodTexts()
            TextBoxPgcPlan.Text = .Plan.Nom
            TextBoxEsp.Text = .Esp
            TextBoxCat.Text = .Cat
            TextBoxEng.Text = .Eng
            TextBoxDsc.Text = .Dsc
            If .Epg IsNot Nothing Then
                TextBoxEpg.Text = .Epg.Nom
            End If

            If .IsMainGrup Then
                GroupBoxBalCod.Visible = True
                RadioButtonCod0.Checked = (.BalCod = MaxiSrvr.PgcGrup.BalCods.balanç)
                RadioButtonCod1.Checked = (.BalCod = MaxiSrvr.PgcGrup.BalCods.explotacio)
            Else
                GroupBoxBalCod.Visible = False
            End If
        End With
    End Sub

    Private Sub SetCodTexts()
        If TextBoxId.Text.Trim.Length = 1 Then
            RadioButtonCod0.Text = "balanç"
            RadioButtonCod1.Text = "compte de explotació"
        Else
            Dim oGrup As New PgcGrup(mPgcGrup.Plan, TextBoxId.Text.Trim)
            Select Case oGrup.BalCod
                Case MaxiSrvr.PgcGrup.BalCods.balanç
                    RadioButtonCod0.Text = "actiu"
                    RadioButtonCod1.Text = "passiu"
                Case MaxiSrvr.PgcGrup.BalCods.explotacio
                    RadioButtonCod0.Text = "despeses"
                    RadioButtonCod1.Text = "ingressos"
            End Select
        End If
    End Sub

    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    TextBoxEsp.TextChanged, _
    TextBoxCat.TextChanged, _
    TextBoxEng.TextChanged, _
    TextBoxDsc.TextChanged, _
     RadioButtonCod0.CheckedChanged, _
      RadioButtonCod1.CheckedChanged
        ButtonOk.Enabled = True
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mPgcGrup
            .Id = TextBoxId.Text
            .Esp = TextBoxEsp.Text
            .Cat = TextBoxCat.Text
            .Eng = TextBoxEng.Text
            .Dsc = TextBoxDsc.Text
            If .IsMainGrup Then
                .BalCod = IIf(RadioButtonCod0.Checked, 0, 1)
            End If
            .Update()
        End With
        RaiseEvent AfterUpdate(mPgcGrup, e)
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim sErr As String = ""
        Dim rc As MsgBoxResult = MsgBox("eliminem aquest pla?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            If mPgcGrup.Delete(sErr) Then
                RaiseEvent AfterUpdate(sender, e)
                Me.Close()
            Else
                MsgBox(sErr, MsgBoxStyle.Exclamation)
            End If
        End If
    End Sub

    Private Sub TextBoxEpg_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxEpg.DoubleClick
        If mPgcGrup.Epg IsNot Nothing Then
            ShowEpg(mPgcGrup.Epg)
        End If
    End Sub

    Private Sub ShowEpg(ByVal oEpg As PgcEpg)
        Dim oFrm As New Frm_PgcEpg
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestEpg
        With oFrm
            .Epg = oEpg
            .Show()
        End With
    End Sub

    Private Sub Refreshrequestepg(ByVal sender As Object, ByVal e As System.EventArgs)
        If mPgcGrup.Epg IsNot Nothing Then
            TextBoxEpg.Text = mPgcGrup.Epg.Nom
        End If
    End Sub
End Class
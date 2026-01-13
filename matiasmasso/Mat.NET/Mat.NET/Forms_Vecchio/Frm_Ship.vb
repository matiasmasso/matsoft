

Public Class Frm_Ship
    Private mShip As Ship
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oShip As Ship)
        MyBase.new()
        Me.InitializeComponent()
        mShip = oShip
        'Me.Text = mObject.ToString
        Refresca()
        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        With mShip
            'TextBoxNom.Text = .text
            If .Exists Then
                TextBoxMMSI.Enabled = False
                ButtonDel.Enabled = .AllowDelete
            End If
        End With
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxNom.TextChanged
        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        If TextBoxMMSI.Text > 0 Then
            mShip = New Ship(TextBoxMMSI.Text)
            With mShip
                .Nom = TextBoxNom.Text
                .Update()
                RaiseEvent AfterUpdate(mShip, System.EventArgs.Empty)
                Me.Close()
            End With
        Else
            MsgBox("es obligatori entrar el MMSI (Maritime Mobile Service Identity)")
        End If
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        If mShip.AllowDelete Then
            mShip.Delete()
            Me.Close()
        End If
    End Sub

    Private Sub ButtonBrowse_Click(sender As Object, e As System.EventArgs) Handles ButtonBrowse.Click
        UIHelper.ShowHtml(mShip.UrlGeo)
    End Sub
End Class
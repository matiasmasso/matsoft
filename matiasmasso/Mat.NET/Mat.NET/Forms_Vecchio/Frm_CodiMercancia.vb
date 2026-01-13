Public Class Frm_CodiMercancia
    Private mCodiMercancia As MaxiSrvr.CodiMercancia
    Private mAllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oCodiMercancia As MaxiSrvr.CodiMercancia)
        MyBase.New()
        Me.InitializeComponent()
        mCodiMercancia = oCodiMercancia
        If mCodiMercancia.IsEmpty Then
            Me.Text = "NOU CODI MERCANCIES"
        Else
            Me.Text = "CODI " & mCodiMercancia.Id
            ButtonDel.Enabled = mCodiMercancia.AllowDelete
            TextBoxCodi.Enabled = False
        End If
        TextBoxCodi.Text = mCodiMercancia.Id
        TextBoxDsc.Text = mCodiMercancia.Dsc
        mAllowEvents = True
    End Sub



    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    TextBoxCodi.TextChanged, TextBoxDsc.TextChanged
        If mallowevents Then
            ButtonOk.Enabled = True
        End If

    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        If mCodiMercancia.IsEmpty Then
            mCodiMercancia = New maxisrvr.CodiMercancia(TextBoxCodi.Text)
            If mCodiMercancia.IsEmpty Then
                MsgBox("no s'admet el codi tot zeros", MsgBoxStyle.Exclamation, "MAT.NET")
                Exit Sub
            End If
        End If

        With mCodiMercancia
            .Dsc = TextBoxDsc.Text
            .update()
        End With

        RaiseEvent AfterUpdate(mCodiMercancia, EventArgs.Empty)
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub TextBoxCodi_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxCodi.Validated
        Dim oTmpCodiMercancia As New maxisrvr.CodiMercancia(TextBoxCodi.Text)
        If oTmpCodiMercancia.Exists Then
            MsgBox("aquest codi ja existeix", MsgBoxStyle.Information, "MAT.NET")
            TextBoxDsc.Text = oTmpCodiMercancia.Dsc
            ButtonOk.Enabled = False
        End If
    End Sub
End Class
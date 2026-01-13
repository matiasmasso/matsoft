

Public Class Frm_GoogleGeocode
    Private mCoordenadas As maxisrvr.GeoCoordenadas
    Private mAllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As System.EventArgs)

    Public Sub New(oCoordenadas As maxisrvr.GeoCoordenadas)
        MyBase.New()
        Me.InitializeComponent()
        mCoordenadas = oCoordenadas
        refresca()
    End Sub

    Private Sub refresca()
        TextBoxLat.Text = mCoordenadas.Latitud
        TextBoxLong.Text = mCoordenadas.Longitud
        mAllowEvents = True
    End Sub

    Private Sub TextBoxLat_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles _
        TextBoxLat.KeyPress, _
         TextBoxLong.KeyPress

        If e.KeyChar = "." Then e.KeyChar = ","
    End Sub


    Private Sub TextBoxLat_TextChanged(sender As Object, e As System.EventArgs) Handles _
        TextBoxLat.TextChanged, _
         TextBoxLong.TextChanged

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As System.EventArgs) Handles ButtonOk.Click
        If IsNumeric(TextBoxLat.Text) And IsNumeric(TextBoxLong.Text) Then
            Dim DcLat As Decimal = CDec(TextBoxLat.Text)
            Dim DcLong As Decimal = CDec(TextBoxLong.Text)
            mCoordenadas = New maxisrvr.GeoCoordenadas(DcLat, DcLong, maxisrvr.GeoCoordenadas.GeoFonts.Manual)
            RaiseEvent AfterUpdate(mCoordenadas, System.EventArgs.Empty)
            Me.Close()
        Else
            MsgBox("latitud o longitud en format incorrecte", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub



    Private Sub TextBoxLat_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TextBoxLat.Validating
        If Not maxisrvr.GeoCoordenadas.IsValidLatitud(TextBoxLat.Text) Then
            e.Cancel = True
            MsgBox("valor incorrecte. Ha de estar entre -90,0 i +90,0", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub

    Private Sub TextBoxLong_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TextBoxLong.Validating
        If Not maxisrvr.GeoCoordenadas.IsValidLongitud(TextBoxLong.Text) Then
            e.Cancel = True
            MsgBox("valor incorrecte. Ha de estar entre -180,0 i +180,0", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub
End Class
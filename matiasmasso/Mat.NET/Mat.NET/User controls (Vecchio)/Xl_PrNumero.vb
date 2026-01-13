

Public Class Xl_PrNumero
    Private _PrNumero As PrNumero = Nothing
    Private mEditorial As PrEditorial = Nothing
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public WriteOnly Property Editorial() As PrEditorial
        Set(ByVal value As PrEditorial)
            mEditorial = value
        End Set
    End Property

    Public Property Numero() As PrNumero
        Get
            Return _PrNumero
        End Get
        Set(ByVal value As PrNumero)
            If value IsNot Nothing Then
                _PrNumero = value
                PrNumeroLoader.load(_PrNumero)
                TextBox1.Text = _PrNumero.FullText
            End If
        End Set
    End Property

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim oFrm As New Frm_PrEditorials(Frm_PrEditorials.Modes.SelectNumero, mEditorial, _PrNumero)
        AddHandler oFrm.AfterSelect, AddressOf RefreshRequest
        oFrm.show()
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        _PrNumero = sender
        TextBox1.Text = _PrNumero.FullText
        RaiseEvent AfterUpdate(_PrNumero, EventArgs.Empty)
    End Sub
End Class



Public Class Frm_Prv_Pdc2
    Private mPdc As Pdc

    Public WriteOnly Property Pdc() As Pdc
        Set(ByVal value As Pdc)
            mPdc = value
            Me.Text = "COMANDA A PROVEIDOR: " & mPdc.Client.Clx

        End Set
    End Property

End Class
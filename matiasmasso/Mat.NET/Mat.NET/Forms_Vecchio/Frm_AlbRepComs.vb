Public Class Frm_AlbRepComs

    Public Sub New(oAlb As Alb)
        MyBase.New()
        Me.InitializeComponent()
        Me.Text = "Comisions sobre albará " & oAlb.Id & " del " & oAlb.Fch.ToShortDateString & " a " & oAlb.Client.Clx
        Xl_AlbRepComs1.LineItmArcs = oAlb.Itms
    End Sub
End Class
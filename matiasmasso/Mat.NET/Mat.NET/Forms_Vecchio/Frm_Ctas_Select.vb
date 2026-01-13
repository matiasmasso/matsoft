Public Class Frm_Ctas_Select

    Public Event onItemSelected(sender As System.Object, e As MatEventArgs)


    Public Sub New(oCtas As PgcCtas)
        MyBase.New()
        Me.InitializeComponent()
        Xl_Ctas1.DataSource = oCtas

        Me.Width = Me.Width + Xl_Ctas1.WidthAdjustment
        Me.Height = Xl_Ctas1.AdjustedHeight
    End Sub

    Public ReadOnly Property SelectedObject As PgcCta
        Get
            Dim retval As PgcCta = Xl_Ctas1.selectedObject
            Return retval
        End Get
    End Property


    Private Sub Xl_Ctas1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Ctas1.onItemSelected
        RaiseEvent onItemSelected(sender, e)
        Me.Close()
    End Sub
End Class
Public Class Frm_Ctas_Select

    Public Event onItemSelected(sender As System.Object, e As MatEventArgs)


    Public Sub New(oCtas As List(Of DTOPgcCta))
        MyBase.New()
        Me.InitializeComponent()
        Xl_PgcCtas1.Load(oCtas,, DTO.Defaults.SelectionModes.Selection)

        Me.Width = Me.Width + Xl_PgcCtas1.WidthAdjustment
        Me.Height = Xl_PgcCtas1.AdjustedHeight
    End Sub

    Public ReadOnly Property SelectedObject As DTOPgcCta
        Get
            Dim retval As DTOPgcCta = Xl_PgcCtas1.Value
            Return retval
        End Get
    End Property


    Private Sub Xl_PgcCtas1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_PgcCtas1.onItemSelected
        RaiseEvent onItemSelected(sender, e)
        Me.Close()
    End Sub
End Class
Public Class Frm_Skus_Select
    Public Event onItemSelected(sender As System.Object, e As MatEventArgs)


    Public Sub New(value As List(Of DTOProductSku))
        MyBase.New()
        Me.InitializeComponent()
        Xl_Skus_Selection1.Load(value)

        Me.Width = Me.Width + Xl_Skus_Selection1.WidthAdjustment
        Me.Height = Xl_Skus_Selection1.AdjustedHeight
    End Sub

    Public Sub New(value As List(Of ProductSku))
        MyBase.New()
        Me.InitializeComponent()
        Xl_Skus_Selection1.Load(value)

        Me.Width = Me.Width + Xl_Skus_Selection1.WidthAdjustment
        Me.Height = Xl_Skus_Selection1.AdjustedHeight
    End Sub

    Public ReadOnly Property SelectedObject As DTOProductSku
        Get
            Dim retval As DTOProductSku = Xl_Skus_Selection1.CurrentItem
            Return retval
        End Get
    End Property

    Public ReadOnly Property SelectedObjectOld As ProductSku 'TO DEPRECATE
        Get
            Dim retval As ProductSku = Xl_Skus_Selection1.CurrentItemOld
            Return retval
        End Get
    End Property

    Private Sub Xl_Arts_Selection1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Skus_Selection1.onItemSelected
        RaiseEvent onItemSelected(sender, e)
        Me.Close()
    End Sub
End Class
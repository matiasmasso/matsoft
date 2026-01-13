

Public Class Xl_Currency

    Private _AllowEvents As Boolean
    Public Event AfterUpdate(sender As Object, e As EventArgs)

    Public Sub New()
        MyBase.New()
        Me.InitializeComponent()
        FillWithISOCurrencySymbols(ComboBox1)
        _AllowEvents = True
    End Sub

    Public Property Currency As DTOCurrency
        Get
            Dim retval As DTOCurrency = ComboBox1.SelectedItem
            Return retval
        End Get
        Set(value As DTOCurrency)
            If value IsNot Nothing Then
                ComboBox1.SelectedValue = value.ISO
            End If
        End Set
    End Property

    Public Shared Sub FillWithISOCurrencySymbols(oComboBox As ComboBox)
        With oComboBox
            .DataSource = BLL_Currencies.All(BLL.BLLSession.Current.Lang)
            .DisplayMember = "Nom"
            .ValueMember = "ISO"
            .SelectedValue = BLL_Currency.Current
        End With
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If _AllowEvents Then
            RaiseEvent AfterUpdate(Me.Currency, EventArgs.Empty)
        End If
    End Sub
End Class

Public Class Frm_BancPool

    Private _BancPool As DTOBancPool
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOBancPool)
        MyBase.New()
        Me.InitializeComponent()
        _BancPool = value
        BLL.BLLBancPool.Load(_BancPool)
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        UIHelper.LoadComboFromEnum(ComboBoxProductCategories, GetType(DTOBancPool.ProductCategories))
        With _BancPool
            Xl_LookupBank1.Bank = .Bank
            ComboBoxProductCategories.SelectedIndex = .ProductCategory
            DateTimePickerFchFrom.Value = .Fch
            Xl_Eur1.Amt = .Amt
            TextBoxObs.Text = .Obs
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvents = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
             Xl_LookupBank1.AfterUpdate,
        ComboBoxProductCategories.SelectedIndexChanged,
          DateTimePickerFchFrom.ValueChanged,
           Xl_Eur1.AfterUpdate,
            TextBoxObs.TextChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _BancPool
            .Bank = Xl_LookupBank1.Bank
            .ProductCategory = ComboBoxProductCategories.SelectedIndex
            .Fch = DateTimePickerFchFrom.Value
            .Amt = Xl_Eur1.Amt
            .Obs = TextBoxObs.Text
        End With

        Dim exs As New List(Of Exception)
        If BLL.BLLBancPool.Update(_BancPool, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_BancPool))
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al desar el pool bancari")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If BLL.BLLBancPool.Delete(_BancPool, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_BancPool))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub


End Class



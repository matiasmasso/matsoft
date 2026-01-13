Public Class Frm_IbanStructure

    Private _IbanStructure As DTOIbanStructure
    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOIbanStructure)
        MyBase.New()
        Me.InitializeComponent()
        _IbanStructure = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _IbanStructure
            BLL.BLLCountry.Load(.Country)
            TextBoxCountry.Text = BLL.BLLCountry.Nom(.Country, BLL.BLLApp.Lang)

            NumericUpDownBankPos.Value = .BankPosition
            NumericUpDownBankLen.Value = .BankPosition
            RadioButtonBankNum.Checked = (.BankFormat = DTOIbanStructure.Formats.Numeric)
            RadioButtonBankAlfa.Checked = (.BankFormat = DTOIbanStructure.Formats.Alfanumeric)

            NumericUpDownBranchPos.Value = .BranchPosition
            NumericUpDownBranchLen.Value = .BranchLength
            RadioButtonBranchNum.Checked = (.BranchFormat = DTOIbanStructure.Formats.Numeric)
            RadioButtonBranchAlfa.Checked = (.BranchFormat = DTOIbanStructure.Formats.Alfanumeric)

            NumericUpDownDcPos.Value = .CheckDigitsPosition
            NumericUpDownDcLen.Value = .CheckDigitsLength
            RadioButtonDcNum.Checked = (.CheckDigitsFormat = DTOIbanStructure.Formats.Numeric)
            RadioButtonDcAlfa.Checked = (.CheckDigitsFormat = DTOIbanStructure.Formats.Alfanumeric)

            NumericUpDownCccPos.Value = .AccountPosition
            NumericUpDownCccLen.Value = .AccountLength
            RadioButtonCccNum.Checked = (.AccountFormat = DTOIbanStructure.Formats.Numeric)
            RadioButtonCccAlfa.Checked = (.AccountFormat = DTOIbanStructure.Formats.Alfanumeric)

            NumericUpDownOverallLength.Value = .OverallLength

            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvent = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        NumericUpDownBankPos.ValueChanged, _
         NumericUpDownBankLen.ValueChanged, _
          NumericUpDownBranchPos.ValueChanged, _
           NumericUpDownBranchLen.ValueChanged, _
            NumericUpDownDcPos.ValueChanged, _
             NumericUpDownDcLen.ValueChanged, _
              NumericUpDownCccPos.ValueChanged, _
               NumericUpDownCccLen.ValueChanged, _
                NumericUpDownOverallLength.ValueChanged, _
                 RadioButtonBankNum.CheckedChanged, _
                  RadioButtonBranchNum.CheckedChanged, _
                   RadioButtonDcNum.CheckedChanged, _
                    RadioButtonCccNum.CheckedChanged

        If _AllowEvent Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _IbanStructure
            .BankPosition = NumericUpDownBankPos.Value
            .BankPosition = NumericUpDownBankLen.Value
            .BankFormat = IIf(RadioButtonBankNum.Checked, DTOIbanStructure.Formats.Numeric, DTOIbanStructure.Formats.Alfanumeric)

            .BranchPosition = NumericUpDownBranchPos.Value
            .BranchLength = NumericUpDownBranchLen.Value
            .BranchFormat = IIf(RadioButtonBranchNum.Checked, DTOIbanStructure.Formats.Numeric, DTOIbanStructure.Formats.Alfanumeric)

            .CheckDigitsPosition = NumericUpDownDcPos.Value
            .CheckDigitsLength = NumericUpDownDcLen.Value
            .CheckDigitsFormat = IIf(RadioButtonDcNum.Checked, DTOIbanStructure.Formats.Numeric, DTOIbanStructure.Formats.Alfanumeric)

            .AccountPosition = NumericUpDownCccPos.Value
            .AccountLength = NumericUpDownCccLen.Value
            .AccountFormat = IIf(RadioButtonCccNum.Checked, DTOIbanStructure.Formats.Numeric, DTOIbanStructure.Formats.Alfanumeric)

            .OverallLength = NumericUpDownOverallLength.Value
        End With

        Dim exs as New List(Of exception)
        If BLL.BLLIbanStructure.Update(_IbanStructure, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_IbanStructure))
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs as New List(Of exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If BLL.BLLIbanStructure.Delete(_IbanStructure, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_IbanStructure))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub


End Class



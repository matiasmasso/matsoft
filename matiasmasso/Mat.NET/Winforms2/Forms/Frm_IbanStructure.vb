Public Class Frm_IbanStructure

    Private _IbanStructure As DTOIban.Structure
    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOIban.Structure)
        MyBase.New()
        Me.InitializeComponent()
        _IbanStructure = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.Country.Load(_IbanStructure.Country, exs) Then
            With _IbanStructure
                TextBoxCountry.Text = DTOCountry.NomTraduit(.Country, DTOApp.current.lang)

                NumericUpDownBankPos.Value = .BankPosition
                NumericUpDownBankLen.Value = .BankPosition
                RadioButtonBankNum.Checked = (.BankFormat = DTOIban.Structure.Formats.Numeric)
                RadioButtonBankAlfa.Checked = (.BankFormat = DTOIban.Structure.Formats.Alfanumeric)

                NumericUpDownBranchPos.Value = .BranchPosition
                NumericUpDownBranchLen.Value = .BranchLength
                RadioButtonBranchNum.Checked = (.BranchFormat = DTOIban.Structure.Formats.Numeric)
                RadioButtonBranchAlfa.Checked = (.BranchFormat = DTOIban.Structure.Formats.Alfanumeric)

                NumericUpDownDcPos.Value = .CheckDigitsPosition
                NumericUpDownDcLen.Value = .CheckDigitsLength
                RadioButtonDcNum.Checked = (.CheckDigitsFormat = DTOIban.Structure.Formats.Numeric)
                RadioButtonDcAlfa.Checked = (.CheckDigitsFormat = DTOIban.Structure.Formats.Alfanumeric)

                NumericUpDownCccPos.Value = .AccountPosition
                NumericUpDownCccLen.Value = .AccountLength
                RadioButtonCccNum.Checked = (.AccountFormat = DTOIban.Structure.Formats.Numeric)
                RadioButtonCccAlfa.Checked = (.AccountFormat = DTOIban.Structure.Formats.Alfanumeric)

                NumericUpDownOverallLength.Value = .OverallLength

                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvent = True
        Else
            UIHelper.WarnError(exs)
        End If

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

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _IbanStructure
            .BankPosition = NumericUpDownBankPos.Value
            .BankLength = NumericUpDownBankLen.Value
            .BankFormat = IIf(RadioButtonBankNum.Checked, DTOIban.Structure.Formats.Numeric, DTOIban.Structure.Formats.Alfanumeric)

            .BranchPosition = NumericUpDownBranchPos.Value
            .BranchLength = NumericUpDownBranchLen.Value
            .BranchFormat = IIf(RadioButtonBranchNum.Checked, DTOIban.Structure.Formats.Numeric, DTOIban.Structure.Formats.Alfanumeric)

            .CheckDigitsPosition = NumericUpDownDcPos.Value
            .CheckDigitsLength = NumericUpDownDcLen.Value
            .CheckDigitsFormat = IIf(RadioButtonDcNum.Checked, DTOIban.Structure.Formats.Numeric, DTOIban.Structure.Formats.Alfanumeric)

            .AccountPosition = NumericUpDownCccPos.Value
            .AccountLength = NumericUpDownCccLen.Value
            .AccountFormat = IIf(RadioButtonCccNum.Checked, DTOIban.Structure.Formats.Numeric, DTOIban.Structure.Formats.Alfanumeric)

        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB.IbanStructure.Update(_IbanStructure, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_IbanStructure))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB.IbanStructure.Delete(_IbanStructure, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_IbanStructure))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub


End Class



Imports System.ComponentModel

Public Class Frm_PgcCta

    Private _Cta As DTOPgcCta
    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOPgcCta)
        MyBase.New()
        Me.InitializeComponent()
        _Cta = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.PgcCta.Load(_Cta, exs) Then
            With _Cta
                If .IsNew Then
                    Me.Text = "Nou compte"
                Else
                    Dim oLang As DTOLang = Current.Session.Lang
                    Me.Text = DTOPgcCta.FullNom(_Cta, oLang)
                End If
                Xl_LookupPgcClass1.PgcClass = .PgcClass
                TextBoxEsp.Text = .Nom.Esp
                TextBoxCat.Text = .Nom.Cat
                TextBoxEng.Text = .Nom.Eng
                TextBoxId.Text = .Id
                ComboBoxDh.SelectedIndex = .Act
                LoadCodis(.Codi)
                CheckBoxIsBaseImponibleIva.Checked = .isBaseImponibleIva
                CheckBoxIsQuotaIva.Checked = .isQuotaIva
                CheckBoxIsQuotaIrpf.Checked = .isQuotaIrpf

                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvent = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub LoadCodis(oCodCta As DTOPgcPlan.Ctas)
        UIHelper.LoadComboFromEnum(ComboBoxCodi, GetType(DTOPgcPlan.Ctas),, oCodCta)
        ComboBoxCodi.Enabled = Current.Session.Rol.IsSuperAdmin
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxEsp.TextChanged,
        TextBoxCat.TextChanged,
          TextBoxEng.TextChanged,
            Xl_LookupPgcClass1.AfterUpdate,
             ComboBoxDh.SelectedIndexChanged,
              ComboBoxCodi.SelectedIndexChanged,
               CheckBoxIsBaseImponibleIva.CheckedChanged,
                CheckBoxIsQuotaIva.CheckedChanged,
                 CheckBoxIsQuotaIrpf.CheckedChanged

        If _AllowEvent Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        With _Cta
            .Id = TextBoxId.Text
            .PgcClass = Xl_LookupPgcClass1.PgcClass
            .Nom = New DTOLangText(TextBoxEsp.Text, TextBoxCat.Text, TextBoxEng.Text)
            .Act = ComboBoxDh.SelectedIndex
            .Codi = ComboBoxCodi.SelectedValue
            .IsBaseImponibleIva = CheckBoxIsBaseImponibleIva.Checked
            .isQuotaIva = CheckBoxIsQuotaIva.Checked
            .isQuotaIrpf = CheckBoxIsQuotaIrpf.Checked
        End With

        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB.PgcCta.Update(_Cta, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Cta))
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
            If Await FEB.PgcCta.Delete(_Cta, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Cta))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub


    Private Async Sub TextBoxId_Validating(sender As Object, e As CancelEventArgs) Handles TextBoxId.Validating
        Dim exs As New List(Of Exception)
        Dim sId As String = TextBoxId.Text
        If TextHelper.RegexMatch(sId, "^\d{5}$") Then
            Dim oCta = Await FEB.PgcCta.FromId(_Cta.Plan, sId, exs)
            If exs.Count = 0 Then
                If oCta IsNot Nothing Then
                    If _Cta.UnEquals(oCta) Then
                        MsgBox("Ja existeix un compte amb aquest número:" & vbCrLf & DTOPgcCta.FullNom(oCta, Current.Session.Lang), MsgBoxStyle.Exclamation)
                        e.Cancel = True
                    End If
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            MsgBox("el compte ha de tenir 5 digits numerics", MsgBoxStyle.Exclamation)
            e.Cancel = True
        End If
    End Sub


End Class



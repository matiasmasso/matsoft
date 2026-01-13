

Public Class Xl_Contact2_Gral
    Implements IUpdatableDetailsPanel

    Private mContact As Contact

    Private mDirtyGral As Boolean = False
    Private mDirtyClx As Boolean = False
    Private mDirtyCll As Boolean = False
    Private mDirtyAdrPostal As Boolean = False 'per el checkbox nomes

    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Implements IUpdatableDetailsPanel.AfterUpdate

    Public Sub New(ByVal oContact As Contact)
        MyBase.New()
        Me.InitializeComponent()
        mContact = oContact

        mAllowEvents = False
        With mContact
            TextBoxRaoSocial.Text = .Nom
            TextBoxNomComercial.Text = .NomComercial
            TextBoxSearchKey.Text = .NomKey
            Xl_AdrFiscal.Adr = .Adr
            Dim oAdrPostal As Adr = .GetAdr(Adr.Codis.Correspondencia)
            If Not oAdrPostal.IsEmpty Then
                If Not .Adr.Equals(oAdrPostal) Then
                    CheckBoxAdrPostal.Checked = True
                    Xl_AdrPostal.Adr = oAdrPostal
                End If
            End If

            Xl_Rol1.Rol = .Rol
            Xl_NIF1.Nif = New DTONif(.NIF)
            Xl_EanGln.Ean13 = .Gln
            If Not oAdrPostal.isempty Then
                Xl_AdrPostal.Visible = True
                Xl_AdrPostal.Adr = oAdrPostal
            End If
            Xl_Langs1.Lang = .Lang
            TextBoxWeb.Text = .Web
            Xl_TelsMailsAndContacts1.Contact = mContact
            CheckBoxObsoleto.Checked = .Obsoleto
            Xl_ContactNewFitxa.Contact = .ContactNou
            Xl_ContactOldFitxa.Contact = .ContactAnterior
        End With
        mAllowEvents = True
    End Sub

    Public ReadOnly Property DirtyGral() As Boolean
        Get
            Return mDirtyGral
        End Get
    End Property

    Public ReadOnly Property DirtyClx() As Boolean
        Get
            Return mDirtyClx
        End Get
    End Property

    Public ReadOnly Property DirtyCll() As Boolean
        Get
            Return mDirtyCll
        End Get
    End Property

    Private Sub GRAL_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    Xl_Rol1.AfterUpdate, _
    Xl_NIF1.Changed, _
    Xl_EanGln.Changed, _
    Xl_Langs1.AfterUpdate, _
    Xl_TelsMailsAndContacts1.AfterUpdate, _
    Xl_ContactNewFitxa.AfterUpdate, _
    Xl_ContactOldFitxa.AfterUpdate

        SetDirtyGral()
    End Sub



    Private Sub GRAL_CLL_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxSearchKey.TextChanged

        SetDirtyGral()
        SetDirtyCll()
    End Sub

    Private Sub GRAL_CLX_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        CheckBoxObsoleto.CheckedChanged

        SetDirtyGral()
        SetDirtyClx()
    End Sub

    Private Sub CLL_CLX_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        Xl_AdrFiscal.AfterUpdate

        SetDirtyCll()
        SetDirtyClx()
    End Sub

    Private Sub GRAL_CLX_CLL_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxRaoSocial.TextChanged, _
        TextBoxNomComercial.TextChanged

        SetDirtyGral()
        SetDirtyCll()
        SetDirtyClx()
    End Sub


    Private Sub SetDirtyGral()
        If mAllowEvents Then
            mDirtyGral = True
            RaiseEvent AfterUpdate(Nothing, EventArgs.Empty)
        End If
    End Sub

    Private Sub SetDirtyClx()
        If mAllowEvents Then
            mDirtyClx = True
            RaiseEvent AfterUpdate(Nothing, EventArgs.Empty)
        End If
    End Sub

    Private Sub SetDirtyCll()
        If mAllowEvents Then
            mDirtyCll = True
            RaiseEvent AfterUpdate(Nothing, EventArgs.Empty)
        End If
    End Sub

    Private Sub SetDirtyAdrPostal()
        If mAllowEvents Then
            mDirtyAdrPostal = True
            RaiseEvent AfterUpdate(Nothing, EventArgs.Empty)
        End If
    End Sub

    Public Function UpdateIfDirty(ByRef exs as List(Of exception)) As Boolean Implements IUpdatableDetailsPanel.UpdateIfDirty
        Dim retval As Boolean = False
        With mContact
            If mDirtyGral Then
                .Nom = TextBoxRaoSocial.Text
                .NomComercial = TextBoxNomComercial.Text
                .NomKey = TextBoxSearchKey.Text
                .Rol = Xl_Rol1.Rol
                .NIF = Xl_NIF1.Nif.Value
                .Gln = Xl_EanGln.Ean13
                .Lang = Xl_Langs1.Lang
                .Obsoleto = CheckBoxObsoleto.Checked
                .ContactNou = Xl_ContactNewFitxa.Contact
                .ContactAnterior = Xl_ContactOldFitxa.Contact
                .UpdateGral( exs)
                retval = True
            End If

            If Xl_AdrFiscal.IsDirty Then
                .Adr = Xl_AdrFiscal.Adr
                .Adr.Update(mContact, Adr.Codis.Fiscal)
                retval = True
            End If

            If mDirtyAdrPostal Then
                If CheckBoxAdrPostal.Checked Then
                    .Adr = Xl_AdrPostal.Adr
                    .Adr.Update(mContact, Adr.Codis.Correspondencia)
                Else
                    Adr.Delete(mContact, Adr.Codis.Correspondencia)
                End If
                retval = True
            End If

            Xl_TelsMailsAndContacts1.UpdateIfDirty( exs)

            If mDirtyClx Then
                .UpdateClx( exs)
                retval = True
            End If

            If mDirtyCll Then
                .UpdateCll( exs)
                retval = True
            End If
        End With

        Return retval
    End Function

    Public Function AllowDelete(ByRef exs as List(Of exception)) As Boolean Implements IUpdatableDetailsPanel.AllowDelete
        Dim retval As Boolean = False
        Return retval
    End Function

    Public Function Delete(ByRef exs as List(Of exception)) As Boolean Implements IUpdatableDetailsPanel.Delete
        Dim retval As Boolean = False
        Return retval
    End Function

    Private Sub CheckBoxAdrPostal_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    CheckBoxAdrPostal.CheckedChanged, _
    Xl_AdrPostal.AfterUpdate

        Xl_AdrPostal.Visible = CheckBoxAdrPostal.Checked
        SetDirtyAdrPostal()
    End Sub
End Class

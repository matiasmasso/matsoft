

Public Class Xl_Contact2_Proveidor
    Implements IUpdatableDetailsPanel

    Private mProveidor As Proveidor
    Private mLoadedIncoterms As Boolean

    Private mAllowEvents As Boolean = False
    Private mDirty As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Implements IUpdatableDetailsPanel.AfterUpdate

    Public Sub New(ByVal oProveidor As Proveidor)
        MyBase.New()
        Me.InitializeComponent()
        mProveidor = oProveidor
        With mProveidor
            Xl_Cta1.Cta = .DefaultCtaCarrec
            Xl_Cur1.Cur = .DefaultCur
            Xl_FormaDePago1.LoadFromContact(DTOIban.Cods.Proveidor, oProveidor, .FormaDePago)

            Dim BlIncoterms As Boolean = False
            If .Incoterm IsNot Nothing Then
                BlIncoterms = .Incoterm.Exists
            End If

            If BlIncoterms Then
                LoadIncoterms()
                CheckBoxImportPrv.Checked = True
                GroupBoxImport.Visible = True
                ComboBoxIncoterms.SelectedValue = .Incoterm.Id
                Xl_CodiMercancia1.CodiMercancia = .CodiMercancia
            Else
                CheckBoxImportPrv.Checked = False
                GroupBoxImport.Visible = False
            End If

        End With
        mAllowEvents = True
    End Sub

    Public ReadOnly Property Dirty() As Boolean
        Get
            Return mDirty
        End Get
    End Property

    Private Sub SetDirty()
        mDirty = True
        RaiseEvent AfterUpdate(Nothing, EventArgs.Empty)
    End Sub

    Public Function UpdateIfDirty(ByRef exs as List(Of exception)) As Boolean Implements IUpdatableDetailsPanel.UpdateIfDirty
        If mDirty Then
            With mProveidor
                .DefaultCtaCarrec = Xl_Cta1.Cta
                .DefaultCur = Xl_Cur1.Cur
                If CheckBoxImportPrv.Checked Then
                    .Incoterm = New maxisrvr.IncoTerm(ComboBoxIncoterms.SelectedValue)
                    .CodiMercancia = Xl_CodiMercancia1.CodiMercancia
                Else
                    .Incoterm = New maxisrvr.IncoTerm(New String(" ", 3))
                    .CodiMercancia = New maxisrvr.CodiMercancia(New String("0", 8))
                End If
                .FormaDePago = Xl_FormaDePago1.FormaDePago
                .UpdatePrv()
            End With
        End If
        Return False
    End Function

    Public Function AllowDelete(ByRef exs as List(Of exception)) As Boolean Implements IUpdatableDetailsPanel.AllowDelete
        Dim retval As Boolean = False
        Return retval
    End Function

    Public Function Delete(ByRef exs as List(Of exception)) As Boolean Implements IUpdatableDetailsPanel.Delete
        Dim retval As Boolean = False

        Return retval
    End Function

    Private Sub LoadIncoterms()
        Dim SQL As String = "SELECT ID FROM INCOTERMS ORDER BY ID"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        With ComboBoxIncoterms
            .ValueMember = "ID"
            .DisplayMember = "ID"
            .DataSource = oDs.Tables(0)
        End With
        mLoadedIncoterms = True
    End Sub


    Private Sub Control_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    Xl_Cta1.AfterUpdate, _
     Xl_Cur1.AfterUpdate, _
      CheckBoxImportPrv.CheckedChanged, _
       ComboBoxIncoterms.SelectedIndexChanged, _
        Xl_CodiMercancia1.AfterUpdate, _
         Xl_FormaDePago1.AfterUpdate

        If mallowevents Then SetDirty()

    End Sub
End Class

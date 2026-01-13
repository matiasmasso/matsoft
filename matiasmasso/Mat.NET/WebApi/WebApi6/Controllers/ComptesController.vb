Public Class ComptesController
    Inherits _BaseController


    <HttpPost>
    <Route("api/comptes/sumasysaldos")>
    Public Function sumasysaldos(oQuery As DUI.Query) As List(Of DUI.Saldo)
        Dim retval As New List(Of DUI.Saldo)
        Dim oUser As DTOUser = BLLUser.Find(oQuery.user.Guid)

        Dim oExercici As DTOExercici = Nothing
        If oQuery.year = 0 Then
            oExercici = BLLExercici.Current()
        Else
            oExercici = BLLExercici.FromYear(oQuery.year)
        End If

        Dim oSaldos As List(Of DTOPgcSaldo) = BLLSumasYSaldos.Summary(oExercici)
        For Each oSaldo In oSaldos
            Dim oCta As DTOPgcCta = CType(oSaldo.Epg, DTOPgcCta)
            Dim item As New DUI.Saldo
            With item
                .Guid = oSaldo.Epg.Guid
                .Nom = BLLPgcCta.FullNom(oCta, oUser.Lang)
                If oSaldo.IsDeutor Then
                    .Eur = oSaldo.SdoDeudor.Eur
                Else
                    .Eur = oSaldo.SdoCreditor.Eur
                End If
            End With
            retval.Add(item)
        Next
        Return retval
    End Function

    <HttpPost>
    <Route("api/comptes/subctas")>
    Public Function subctas(oQuery As DUI.Query) As List(Of DUI.Saldo)
        Dim retval As New List(Of DUI.Saldo)
        Dim oUser As DTOUser = BLLUser.Find(oQuery.user.Guid)

        Dim oExercici As DTOExercici = Nothing
        If oQuery.year = 0 Then
            oExercici = BLLExercici.Current()
        Else
            oExercici = BLLExercici.FromYear(oQuery.year)
        End If

        Dim oCta As New DTOPgcCta(oQuery.key1.Guid)
        Dim oSaldos As List(Of DTOPgcSaldo) = BLLSumasYSaldos.SubComptes(oExercici, oCta)
        For Each oSaldo In oSaldos
            Dim item As New DUI.Saldo
            With item
                .Guid = oSaldo.Epg.Guid
                .Nom = BLLPgcCta.FullNom(oSaldo.Epg, oUser.Lang)
                If oSaldo.Contact IsNot Nothing Then
                    .Contact = New DUI.Guidnom
                    .Contact.Guid = oSaldo.Contact.Guid
                    .Contact.Nom = oSaldo.Contact.FullNom
                End If
                .Eur = oSaldo.SdoDeudor.Eur
            End With
            retval.Add(item)
        Next
        Return retval
    End Function

    <HttpPost>
    <Route("api/comptes/extracte")>
    Public Function extracte(oQuery As DUI.Query) As List(Of DUI.Ccb)
        Dim retval As New List(Of DUI.Ccb)
        Dim oUser As DTOUser = BLLUser.Find(oQuery.user.Guid)

        Dim oExercici As DTOExercici = Nothing
        If oQuery.year = 0 Then
            oExercici = BLLExercici.Current()
        Else
            oExercici = BLLExercici.FromYear(oQuery.year)
        End If

        Dim oCta As New DTOPgcCta(oQuery.key1.Guid)

        Dim oContact As DTOContact = Nothing
        If oQuery.key2 IsNot Nothing Then
            oContact = New DTOContact(oQuery.key2.Guid)
        End If

        Dim oExtracte As New DTOExtracte()
        With oExtracte
            .Exercici = oExercici
            .Cta = oCta
            .Contact = oContact
        End With

        Dim oCcbs As List(Of DTOCcb) = BLLExtracte.Ccbs(oExtracte)
        For Each oCcb As DTOCcb In oCcbs
            Dim duiCca As New DUI.Cca
            With duiCca
                .Guid = oCcb.Cca.Guid
                .Fch = MyBase.DateFormat(oCcb.Cca.Fch)
                .Concept = oCcb.Cca.Concept
                .DocUrl = BLLDocFile.DownloadUrl(oCcb.Cca.DocFile, True)
            End With

            Dim duiCta As New DUI.Guidnom
            With duiCta
                .Guid = oCcb.Cta.Guid
                .Nom = BLLPgcCta.Nom(oCcb.Cta, oUser.Lang)
            End With

            Dim duiContact As DUI.Guidnom = Nothing
            If oCcb.Contact IsNot Nothing Then
                duiContact = New DUI.Guidnom
                With duiContact
                    .Guid = oCcb.Contact.Guid
                    .Nom = oCcb.Contact.FullNom
                End With
            End If

            Dim duiCcb As New DUI.Ccb
            With duiCcb
                .Cca = duiCca
                .Guid = oCcb.Guid
                .Cta = duiCta
                .Contact = duiContact
                .Eur = oCcb.Amt.Eur
                .Dh = oCcb.Dh
            End With
            retval.Add(duiCcb)
        Next
        Return retval
    End Function

    <HttpPost>
    <Route("api/comptes/cca")>
    Public Function Cca(oQuery As DUI.Query) As DUI.Cca
        Dim retval As DUI.Cca = Nothing
        Dim oUser As DTOUser = BLLUser.Find(oQuery.user.Guid)
        Dim oCca As DTOCca = BLLCca.Find(oQuery.Guid)
        If oCca IsNot Nothing Then
            retval = New DUI.Cca
            With retval
                .Guid = oCca.Guid
                .Fch = MyBase.DateFormat(oCca.Fch)
                .Concept = oCca.Concept
                .DocUrl = BLLDocFile.DownloadUrl(oCca.DocFile, True)
                .items = New List(Of DUI.Ccb)
            End With

            For Each oCcb As DTOCcb In oCca.Items
                Dim duiCta As New DUI.Guidnom
                With duiCta
                    .Guid = oCcb.Cta.Guid
                    .Nom = BLLPgcCta.FullNom(oCcb.Cta, oUser.Lang)
                End With

                Dim duiContact As DUI.Guidnom = Nothing
                If oCcb.Contact IsNot Nothing Then
                    duiContact = New DUI.Guidnom
                    With duiContact
                        .Guid = oCcb.Contact.Guid
                        .Nom = oCcb.Contact.FullNom
                    End With
                End If

                Dim duiCcb As New DUI.Ccb
                With duiCcb
                    '.Cca = retval
                    .Guid = oCcb.Guid
                    .Cta = duiCta
                    .Contact = duiContact
                    .Eur = oCcb.Amt.Eur
                    .Dh = oCcb.Dh
                End With

                retval.items.Add(duiCcb)
            Next


        End If

        Return retval
    End Function

    Private Sub tmp(oCca As DTOCca, oUser As DTOUser, retval As DUI.Cca)
        For Each oCcb As DTOCcb In oCca.Items
            Dim duiCta As New DUI.Guidnom
            With duiCta
                .Guid = oCcb.Cta.Guid
                .Nom = BLLPgcCta.FullNom(oCcb.Cta, oUser.Lang)
            End With

            Dim duiContact As DUI.Guidnom = Nothing
            If oCcb.Contact IsNot Nothing Then
                duiContact = New DUI.Guidnom
                With duiContact
                    .Guid = oCcb.Contact.Guid
                    .Nom = oCcb.Contact.FullNom
                End With
            End If

            Dim duiCcb As New DUI.Ccb
            With duiCcb
                .Cca = retval
                .Guid = oCcb.Guid
                .Cta = duiCta
                .Contact = duiContact
                .Eur = oCcb.Amt.Eur
                .Dh = oCcb.Dh
            End With

            retval.items.Add(duiCcb)
        Next
    End Sub


End Class

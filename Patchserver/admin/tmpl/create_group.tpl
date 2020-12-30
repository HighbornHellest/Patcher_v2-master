<h1>Neue Gruppe anlegen</h1>
{if $Rights["can_manage_groups"] eq "1"}
	<table>
		<tr>
			<td>Gruppenname:</td>
			<td><input type="text" id="GroupName" style="text-align:left;"></td>
		</tr>
		<tr>
			<td>Darf Dateien verwalten?</td>
			<td><input type="checkbox" id="can_manage_files"></td>
		</tr>
		<tr>
			<td>Darf Dateien hochladen?</td>
			<td><input type="checkbox" id="can_upload_files"></td>
		</tr>
		<tr>
			<td>Darf Dateien löschen?</td>
			<td><input type="checkbox" id="can_delete_files"></td>
		</tr>
		<tr>
			<td>Darf Clientseitiges löschen verwalten?</td>
			<td><input type="checkbox" id="can_manage_delete_client_files"></td>
		</tr>
		<tr>
			<td>Darf Patchliste verwalten?</td>
			<td><input type="checkbox" id="can_manage_patchlist"></td>
		</tr>
		<tr>
			<td>Darf Patchliste generieren?</td>
			<td><input type="checkbox" id="can_generate_patchlist"></td>
		</tr>
		<tr>
			<td>Darf Patchliste editieren?</td>
			<td><input type="checkbox" id="can_edit_patchlist"></td>
		</tr>
		<tr>
			<td>Darf neue Patcherversion hochladen?</td>
			<td><input type="checkbox" id="can_upload_newpatcher"></td>
		</tr>
		<tr>
			<td>Darf Benutzer verwalten?</td>
			<td><input type="checkbox" id="can_manage_users"></td>
		</tr>
		<tr>
			<td>Darf Gruppen verwalten?</td>
			<td><input type="checkbox" id="can_manage_groups"></td>
		</tr>
	</table>
	<div class="submit" style="margin:0;"><a href="javascript:CreateGroup()">Erstellen</a></div>
{else}
	Du hast keine Rechte für diese Seite!
{/if}
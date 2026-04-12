import { $, apiFetch, renderStatus, getQueryParam } from '/scripts/common.js';
(async function initUserEdit() {
    const id = getQueryParam('id');
    const form = $('#user-form');
    const statusEl = $('#status');
    if (!id) {
        renderStatus(statusEl, 'err', 'Missing ?id in URL.');
        form.querySelectorAll('input,button').forEach(el => el.disabled = true);
        return;
    }
    try {
        const u = await apiFetch(`/users/${encodeURIComponent(id)}`);
        form.username.value = u.username ?? '';
        form.email.value = u.email ?? '';
        renderStatus(statusEl, 'ok', 'Loaded user. You can edit and save.');
    } catch (err) {
        renderStatus(statusEl, 'err', `Failed to load data: ${err.message}`);
        return;
    }
    form.addEventListener('submit', async (ev) => {
        ev.preventDefault();
        const payload = { username: form.username.value.trim(), email: form.email.value.trim() };
        try {
            const updated = await apiFetch(`/users/${encodeURIComponent(id)}`, {
                method: 'PUT', body: JSON.stringify(payload),
            });
            renderStatus(statusEl, 'ok', `Updated user #${updated.id} "${updated.username}".`);
        } catch (err) {
            renderStatus(statusEl, 'err', `Update failed: ${err.message}`);
        }
    });
})();

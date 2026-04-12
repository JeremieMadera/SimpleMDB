import { $, apiFetch, renderStatus, getQueryParam } from '/scripts/common.js';
(async function initActorEdit() {
    const id = getQueryParam('id');
    const form = $('#actor-form');
    const statusEl = $('#status');
    if (!id) {
        renderStatus(statusEl, 'err', 'Missing ?id in URL.');
        form.querySelectorAll('input,button').forEach(el => el.disabled = true);
        return;
    }
    try {
        const a = await apiFetch(`/actors/${encodeURIComponent(id)}`);
        form.name.value = a.name ?? '';
        form.birthDate.value = a.birthDate ? a.birthDate.substring(0, 10) : '';
        renderStatus(statusEl, 'ok', 'Loaded actor. You can edit and save.');
    } catch (err) {
        renderStatus(statusEl, 'err', `Failed to load data: ${err.message}`);
        return;
    }
    form.addEventListener('submit', async (ev) => {
        ev.preventDefault();
        const payload = { name: form.name.value.trim(), birthDate: form.birthDate.value };
        try {
            const updated = await apiFetch(`/actors/${encodeURIComponent(id)}`, {
                method: 'PUT', body: JSON.stringify(payload),
            });
            renderStatus(statusEl, 'ok', `Updated actor #${updated.id} "${updated.name}".`);
        } catch (err) {
            renderStatus(statusEl, 'err', `Update failed: ${err.message}`);
        }
    });
})();

import { $, apiFetch, renderStatus } from '/scripts/common.js';
(async function initActorAdd() {
    const form = $('#actor-form');
    const statusEl = $('#status');
    form.addEventListener('submit', async (ev) => {
        ev.preventDefault();
        const payload = {
            name: form.name.value.trim(),
            birthDate: form.birthDate.value,
        };
        try {
            const created = await apiFetch('/actors', { method: 'POST', body: JSON.stringify(payload) });
            renderStatus(statusEl, 'ok', `Created actor #${created.id} "${created.name}".`);
            form.reset();
        } catch (err) {
            renderStatus(statusEl, 'err', `Create failed: ${err.message}`);
        }
    });
})();

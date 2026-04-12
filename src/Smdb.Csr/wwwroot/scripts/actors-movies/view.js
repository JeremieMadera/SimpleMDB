import { $, apiFetch, renderStatus, getQueryParam } from '/scripts/common.js';
(async function initActorMovieView() {
    const id = getQueryParam('id');
    const statusEl = $('#status');
    if (!id) return renderStatus(statusEl, 'err', 'Missing ?id in URL.');
    try {
        const am = await apiFetch(`/actors-movies/${encodeURIComponent(id)}`);
        $('#am-id').textContent = am.id;
        $('#am-actor').textContent = am.actorId;
        $('#am-movie').textContent = am.movieId;
        $('#am-role').textContent = am.role || '—';
        $('#edit-link').href = `/actors-movies/edit.html?id=${encodeURIComponent(am.id)}`;
        renderStatus(statusEl, 'ok', 'Entry loaded successfully.');
    } catch (err) {
        renderStatus(statusEl, 'err', `Failed to load entry ${id}: ${err.message}`);
    }
})();

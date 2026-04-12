import { $, apiFetch, renderStatus, clearChildren, getQueryParam } from '/scripts/common.js';

// Cache to avoid repeated fetches for same actor/movie
const actorCache = {};
const movieCache = {};

async function getActorName(id) {
    if (actorCache[id]) return actorCache[id];
    try {
        const a = await apiFetch(`/actors/${id}`);
        actorCache[id] = a.name ?? `Actor #${id}`;
    } catch { actorCache[id] = `Actor #${id}`; }
    return actorCache[id];
}

async function getMovieTitle(id) {
    if (movieCache[id]) return movieCache[id];
    try {
        const m = await apiFetch(`/movies/${id}`);
        movieCache[id] = m.title ? `${m.title} (${m.year ?? '—'})` : `Movie #${id}`;
    } catch { movieCache[id] = `Movie #${id}`; }
    return movieCache[id];
}

(async function initActorsMoviesIndex() {
    const page = Math.max(1, Number(getQueryParam('page') || localStorage.getItem('am-page') || '1'));
    const size = Math.min(100, Math.max(1, Number(getQueryParam('size') || localStorage.getItem('am-size') || '9')));
    localStorage.setItem('am-page', page);
    localStorage.setItem('am-size', size);
    const listEl = $('#am-list');
    const statusEl = $('#status');
    const tpl = $('#am-card');

    try {
        const payload = await apiFetch(`/actors-movies?page=${page}&size=${size}`);
        const items = Array.isArray(payload) ? payload : (payload.data || []);
        clearChildren(listEl);

        if (items.length === 0) {
            renderStatus(statusEl, 'warn', 'No entries found for this page.');
        } else {
            renderStatus(statusEl, '', '');

            // Resolve all actor/movie names in parallel
            await Promise.all(items.flatMap(am => [
                getActorName(am.actorId),
                getMovieTitle(am.movieId),
            ]));

            for (const am of items) {
                const frag = tpl.content.cloneNode(true);
                const root = frag.querySelector('.card');
                root.querySelector('.actor-name').textContent = actorCache[am.actorId];
                root.querySelector('.movie-title').textContent = movieCache[am.movieId];
                root.querySelector('.role').textContent = am.role || '—';
                root.querySelector('.btn-view').href = `/actors-movies/view.html?id=${encodeURIComponent(am.id)}`;
                root.querySelector('.btn-edit').href = `/actors-movies/edit.html?id=${encodeURIComponent(am.id)}`;
                root.querySelector('.btn-delete').dataset.id = am.id;
                listEl.appendChild(frag);
            }
        }

        listEl.addEventListener('click', async (ev) => {
            const btn = ev.target.closest('button.btn-delete[data-id]');
            if (!btn) return;
            const id = btn.dataset.id;
            if (!confirm('Delete this entry? This cannot be undone.')) return;
            try {
                await apiFetch(`/actors-movies/${encodeURIComponent(id)}`, { method: 'DELETE' });
                renderStatus(statusEl, 'ok', `Entry ${id} deleted.`);
                setTimeout(() => location.reload(), 2000);
            } catch (err) {
                renderStatus(statusEl, 'err', `Delete failed: ${err.message}`);
            }
        });

        const sizeSelect = document.getElementById('page-size');
        for (const s of [3, 6, 9, 12, 15]) {
            const opt = document.createElement('option');
            opt.value = s; opt.textContent = String(s); opt.selected = (size == s);
            sizeSelect.appendChild(opt);
        }
        sizeSelect.addEventListener('change', () => {
            const params = new URLSearchParams(window.location.search);
            params.set('page', 1); params.set('size', sizeSelect.value);
            localStorage.setItem('am-page', 1); localStorage.setItem('am-size', sizeSelect.value);
            window.location.href = `${window.location.pathname}?${params.toString()}`;
        });

        $('#page-num').textContent = `Page ${page}`;
        const totalPages = payload.meta?.totalPages ?? 1;
        const firstPage = page <= 1, lastPage = page >= totalPages;
        const firstBtn = $('#first'), prevBtn = $('#prev'), nextBtn = $('#next'), lastBtn = $('#last');
        firstBtn.href = `?page=1&size=${size}`;
        prevBtn.href = `?page=${page - 1}&size=${size}`;
        nextBtn.href = `?page=${page + 1}&size=${size}`;
        lastBtn.href = `?page=${totalPages}&size=${size}`;
        firstBtn.classList.toggle('disabled', firstPage); prevBtn.classList.toggle('disabled', firstPage);
        nextBtn.classList.toggle('disabled', lastPage);   lastBtn.classList.toggle('disabled', lastPage);
        firstBtn.setAttribute('onclick', `return ${!firstPage};`); prevBtn.setAttribute('onclick', `return ${!firstPage};`);
        nextBtn.setAttribute('onclick', `return ${!lastPage};`);   lastBtn.setAttribute('onclick', `return ${!lastPage};`);
    } catch (err) {
        renderStatus(statusEl, 'err', `Failed to fetch entries: ${err.message}`);
    }
})();

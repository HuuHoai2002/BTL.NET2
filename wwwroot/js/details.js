import { api_key, base_url } from "./config/index.js";
import { params } from "./utils/getParams.js";
import { renderListMovie, renderMovieDetails } from "./utils/render.js";
import { $ } from "./utils/selector.js";

const movie_details = $(".app-movie-details");
const movie_similar = $(".movie-list");

const type = window.location.pathname.includes("Movies") ? "movie" : "tv";
const id = params.get("id");

const details_url = `${base_url}/${type}/${id}?api_key=${api_key}&language=vi`;
const similar_url = `${base_url}/${type}/${id}/similar?api_key=${api_key}&language=vi`;

async function Main() {
  await renderMovieDetails(details_url, movie_details);
  await renderListMovie(similar_url, movie_similar);
}

window.addEventListener("DOMContentLoaded", Main);

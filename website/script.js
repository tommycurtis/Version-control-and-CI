const vibeRange = document.getElementById('vibeRange');
const vibeScore = document.getElementById('vibeScore');
const vibeMessage = document.getElementById('vibeMessage');
const year = document.getElementById('year');

const vibeTexts = [
  { limit: 20, text: 'Chill and simple — low-maintenance villa life.' },
  { limit: 40, text: 'Smart comfort with cozy Fort Wayne charm.' },
  { limit: 60, text: 'Balanced and practical — great starter villa energy!' },
  { limit: 80, text: 'Upscale taste — room to entertain and relax.' },
  { limit: 100, text: 'Luxury lover mode activated — go big on your dream villa!' }
];

vibeRange.addEventListener('input', () => {
  const value = Number(vibeRange.value);
  vibeScore.textContent = String(value);

  const selected = vibeTexts.find((item) => value <= item.limit);
  vibeMessage.textContent = selected?.text ?? vibeTexts[vibeTexts.length - 1].text;
});

document.querySelectorAll('.interest-btn').forEach((button) => {
  button.addEventListener('click', () => {
    const card = button.closest('.villa-card');
    const villaName = card?.querySelector('h2')?.textContent ?? 'This villa';
    const price = card?.dataset.price ?? 'the listed price';

    button.textContent = 'Saved ✓';
    button.disabled = true;

    alert(`${villaName} (${price}) saved! Call Trent at 260-350-4477 to schedule a tour.`);
  });
});

year.textContent = new Date().getFullYear().toString();

export default function formatDate(dateString: string) {
  const date = new Date(dateString);
  const day = String(date.getDate()).padStart(2, "0");
  const month = String(date.getMonth() + 1).padStart(2, "0");
  const year = date.getFullYear();
  return `${day}.${month}.${year}`;
}

export function convertToISO8601(dateString: string): string {
  const match = dateString.match(/^(\d{2})\.(\d{2})\.(\d{4}) (\d{2}):(\d{2})$/);
  if (!match) {
    throw new Error("Invalid input format. Expected format: dd.mm.yyyy hh:mm");
  }

  const [, day, month, year, hours, minutes] = match;

  const date = new Date(`${year}-${month}-${day}T${hours}:${minutes}:00Z`);

  const isoDate = date.toISOString();

  return isoDate;
}
